Imports Prosegur.Global.GesEfectivo.IAC
Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio
Imports System.Text
Imports System.Text.RegularExpressions
Imports Prosegur.Framework.Dicionario
Imports System.Xml.Serialization
Imports System.IO
Imports System.Web
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Extenciones
Imports EnumeradoresComon = Prosegur.Genesis.Comon.Enumeradores
Imports System.Windows.Forms
Imports System.Runtime.Serialization.Formatters.Binary
Imports System.Reflection
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis

Namespace Aplicacao.Util

    Public Class Utilidad

#Region "Constantes"

        '        'Mensagens 
        '        Public Const InfoMsgBaja As String = "info_msg_baja"
        '        Public Const InfoMsgSeleccionarRegistro As String = "info_msg_seleccionar_registro"
        '        Public Const InfoMsgBajaRegistro As String = "info_msg_baja_registro"
        '        Public Const InfoMsgGrabarRegistro As String = "info_msg_grabar"
        '        Public Const InfoMsgAltaRegistro As String = "info_msg_alta"
        '        Public Const InfoMsgSinRegistro As String = "info_msg_sin_registro"
        '        Public Const InfoMsgMaxRegistro As String = "info_msg_max_registro"
        '        Public Const InfoMsgSairPagina As String = "info_msg_sair_pagina"
        '        Public Const GenOpcionSi As String = "gen_opcion_si"
        '        Public Const GenOpcionNo As String = "gen_opcion_no"
        '        'Número máximos de caracteres a serem exibidos por coluna no GridView
        '        Public Const MaximoCaracteresLinha As Integer = 20
        '        'Número máximos de registros a serem exibidos no GridView
        '        Public Const MaximoRegistrosGrid As Integer = 100
        '        'Quebra de linha
        Public Const LineBreak As String = "<BR/>"

#End Region

#Region "Construtores"

        ''' <summary>
        ''' Contrutor privado
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub New()

        End Sub

#End Region

#Region "Enumerador ação"

        ''' <summary>
        ''' Enumerador com as ações mais comuns utilizada na aplicação
        ''' </summary>
        ''' <remarks></remarks>
        ''' <history>
        ''' [pda] 26/01/2008 Created
        ''' </history>
        Public Enum eAcao As Integer
            Inicial = 0
            NoAction = 1
            Alta = 2
            Baja = 3
            Modificacion = 4
            Consulta = 5
            Busca = 6
        End Enum

        ''' <summary>
        ''' Telas do sistema
        ''' </summary>
        ''' <remarks></remarks>
        ''' <history>
        ''' [octavio.piramo] 10/02/2009 Criado
        ''' [pgoncalves] 13/02/2013 Alterado
        ''' [pgoncalves] 04/03/2013 Alterado
        ''' [danielnunes] 14/05/2013 Alterado
        ''' </history>
        Public Enum eTelas As Integer
            EMBRANCO
            'ERRO
            LOGIN
            MENU
            ABM_PADRAO
            LISTADO_CERTIFICADO_CONSULTAR
            CERTIFICADOS
            NUEVO_DOCUMENTO
            CERTIFICADOS_GENERAR_CONSULTAR
            RESULTADO_CERTIFICADO_CONSULTAR
            NIVEL_SALDOS_CONSULTAR
            SELECCION_SECTOR
            FORMULARIOS
            GRUPO_DOCUMENTO
            DOCUMENTO
            DOCUMENTOS
            CONFIGURACION_FORMULARIOS
            INVENTARIO_BULTO_CONSULTAR
            HISTORICO_INVENTARIO_BULTO_CONSULTAR
            RELATORIO_SALDOS_CLIENTE
            RELATORIO_TRANSACCIONES
            RELATORIO_SEGUIMIENTO_BULTOS
            RELATORIO_SEGUIMIENTO_ELEMENTOS
            RELATORIO_CONTENEDORES
            RELATORIO_CLIENTE_X_CONTENEDORES
            GENERAR_INVENTARIO_CONTENEDOR
            RELATORIO_INVENTARIO_CONTENEDOR
            CONTENEDORES_A_VENCER_VENCIDOS
            CONSULTAR_SALDO
            CONSULTAR_HISTORICOIMPORTESELEMENTOS
            RELATORIO_HISTORICO_SALDOS
            CONFIGURAR_MULTIPLOS_REPORTES
            BUSQUEDA_MULTIPLOS_REPORTES
            CERTIFICADOS_DOCUMENTOS_PENDIENTES_MODIFICAR
            CERTIFICADOS_REPORTE_CONFIGURAR
            MANTENIMIENTO_DOCUMENTOS_HIJOS
            HISTORICO_ELEMENTOS
            ABONO
            CERTIFICADOS_REPORTE
            VISUALIZAR_NOTIFICACIONES
            SALDOS_CONSULTAR_TRANSACCIONES
            PROVISION_EFECTIVO
        End Enum

        ''' <summary>
        ''' Ações disponíveis por telas
        ''' </summary>
        ''' <remarks></remarks>
        ''' <history>
        ''' [octavio.piramo] 10/02/2009 Criado
        ''' </history>
        Public Enum eAcoesTela As Integer
            _CONSULTAR
            _CONSULTAR_DETALLE
            _BUSCAR
            _DAR_ALTA
            _DAR_BAJA
            _MODIFICAR
        End Enum

        ''' <summary>
        ''' Enumerador referente a delegação presente no webconfig utilizada na tela de busca de processos.
        ''' Utiliza apenas estes dois valores como referência:
        '''  * MI_DELEGACION_Y_LA_CENTRAL = Retornar o código presente no webconfig
        '''  * TODAS_LAS_DELEGACIONES = Retornar as delegações presente no banco
        ''' </summary>
        ''' <remarks></remarks>
        Public Enum eVisibilidadProcesos As Integer
            MI_DELEGACION_Y_LA_CENTRAL = 1
            TODAS_LAS_DELEGACION = 2
        End Enum


#End Region

#Region "[Logar erro aplicacao]"

        '        ''' <summary>
        '        ''' Faz o tratamento do retorno das mensagens de erro.
        '        ''' </summary>
        '        ''' <param name="msgErro"></param>
        '        ''' <param name="msgErroExibicao"></param>
        '        ''' <param name="nomeServidor"></param>
        '        ''' <returns></returns>
        '        ''' <remarks></remarks>
        '        ''' <history>
        '        ''' [anselmo.gois] 21/09/2010 - Criado
        '        ''' </history>
        '        Public Shared Function RetornarMensagemErro(msgErro As String, msgErroExibicao As String, _
        '                                                    Optional nomeServidor As String = "", _
        '                                                    Optional traduzir As Boolean = False) As String

        '            If Not String.IsNullOrEmpty(msgErro) Then

        '                Dim palavras() As String = msgErro.Split(" ")

        '                If (From p In palavras Where p.Trim.ToLower = Constantes.CONST_ERRO_ACCESO_LDAP.ToLower).Count > 0 Then

        '                    'Retorna erro de acesso ao servidor de ad
        '                    Return If(traduzir, Tradutor.Traduzir("err_msg_error_03"), "err_msg_error_03")

        '                ElseIf (From p In palavras Where p.Trim.ToLower = Constantes.CONST_ERRO_BANCO.ToLower OrElse _
        '                                                 p.Trim.ToLower = Constantes.CONST_ERRO_BANCO2.ToLower).Count > 0 Then

        '                    'Retorna erro de banco de dados.
        '                    Return If(traduzir, String.Format(Tradutor.Traduzir("err_msg_error_05"), nomeServidor), String.Format("err_msg_error_05", nomeServidor))

        '                ElseIf (From p In palavras Where p.Trim.ToLower = Constantes.CONST_ERRO_404B.ToLower _
        '                        OrElse p.Trim.ToLower = Constantes.CONST_ERRO_404A.ToLower _
        '                        OrElse p.Trim.ToLower = Constantes.CONST_ERRO_DNS.ToLower _
        '                        OrElse p.Trim.ToLower = Constantes.CONST_ERRO_URL.ToLower _
        '                        OrElse p.Trim.ToLower = Constantes.CONST_ERRO_407B.ToLower _
        '                        OrElse p.Trim.ToLower = Constantes.CONST_ERRO_407A.ToLower _
        '                        OrElse p.Trim.ToLower = Constantes.CONST_ERRO_COMUNICACION_LDAP.ToLower).Count > 0 Then

        '                    'Retorna erro de url invalida
        '                    Return If(traduzir, Tradutor.Traduzir("err_msg_error_02"), "err_msg_error_02")

        '                End If

        '            End If

        '            Return msgErroExibicao
        '        End Function

        '        ''' <summary>
        '        ''' Trata o retorno da chamada de um serviço
        '        ''' </summary>
        '        ''' <param name="CodErro"></param>
        '        ''' <param name="MsgError"></param>
        '        ''' <param name="MsgRetorno"></param>
        '        ''' <returns></returns>
        '        ''' <remarks></remarks>
        '        ''' <history>
        '        ''' [octavio.piramo] 04/02/2009 Criado
        '        ''' </history>
        '        Public Shared Function TratarRetornoServico(CodErro As Integer, _
        '                                                    MsgError As String, _
        '                                                    Optional ByRef MsgRetorno As String = "", _
        '                                                    Optional ByRef Traduzir As Boolean = True, _
        '                                                    Optional LoginUsuario As String = "", _
        '                                                    Optional DelegacionUsuario As String = "") As Boolean

        '            Select Case CodErro

        '                Case Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
        '                    Return True

        '                Case Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
        '                    MsgRetorno = Tradutor.Traduzir("err_codigo_erro_ambiente_default")

        '                    ' logar erro no banco
        '                    Aplicacao.Util.Utilidad.LogarErroAplicacao(CodErro, MsgError, String.Empty, LoginUsuario, DelegacionUsuario)

        '                Case (Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT + 1) To Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT
        '                    'Caso a mensagem de erro do negócio não tenha sido informada, então exibe a padrão de negócio.
        '                    If MsgError <> String.Empty Then
        '                        MsgRetorno = MsgError
        '                    Else
        '                        MsgRetorno = Tradutor.Traduzir("err_codigo_erro_negocio_default")
        '                    End If

        '                Case Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_SESION_INVALIDA
        '                    MsgRetorno = Tradutor.Traduzir("err_codigo_erro_negocio_sesion_invalida")

        '                Case Else
        '                    MsgRetorno = Tradutor.Traduzir("err_padrao_aplicacao")

        '            End Select

        '            Traduzir = False
        '            Return False

        '        End Function

        ''' <summary>
        ''' Loga erro passando uma exception
        ''' </summary>
        ''' <param name="Ex"></param>
        ''' <remarks></remarks>
        ''' <history>
        ''' [octavio.piramo] 04/02/2009 Criado
        ''' </history>
        Public Shared Sub LogarErroAplicacao(Ex As Exception)

            LogarErroAplicacao(String.Empty, Ex.Message.ToString, String.Empty, String.Empty, String.Empty)

        End Sub

        ''' <summary>
        ''' Metodo genérico para logar os erros retornados pelos webservices.
        ''' </summary>
        ''' <remarks></remarks>
        ''' <history>
        ''' [octavio.piramo] 29/01/2009 Criado
        ''' </history>
        Public Shared Sub LogarErroAplicacao(CodigoErro As Integer, _
                                             MensErro As String, _
                                             OutrasInformacoes As String, _
                                             LoginUsuario As String, _
                                             Delegacion As String)

            Try

                Dim Peticion As New IAC.ContractoServicio.Log.Peticion
                Dim RespuestaLog As IAC.ContractoServicio.Log.Respuesta
                Peticion.FYHErro = DateTime.UtcNow

                Peticion.LoginUsuario = LoginUsuario
                Peticion.Delegacion = Delegacion
                ' --------------------------------------------------------
                Peticion.DescricionErro = String.Format("{0} - {1}", CodigoErro, MensErro)
                Peticion.Otros = OutrasInformacoes

                ' chama o serviço responsável pela gravação do log e guarda a exceção no event viewer caso ocorra algum problema.
                Dim objProxyLog As New Comunicacion.ProxyLog
                RespuestaLog = objProxyLog.InserirLog(Peticion)

                If CodigoErro <> 0 Then
                    GravarLogErroEventViewer(CodigoErro, MensErro)
                End If

            Catch ex As Exception

                GravarLogErroEventViewer(0, ex.Message)

            End Try

        End Sub

        ''' <summary>
        ''' Grava erro no event viewer
        ''' </summary>
        ''' <param name="CodigoErro"></param>
        ''' <param name="DescricaoErro"></param>
        ''' <remarks></remarks>
        ''' <history>
        ''' [octavio.piramo] 29/01/2009 Criado
        ''' </history>
        Private Shared Sub GravarLogErroEventViewer(CodigoErro As String, _
                                                    DescricaoErro As String)

            Try

                ' criar objeto
                Dim objEventLog As New EventLog

                'Register the Application as an Event Source
                If Not EventLog.SourceExists(Prosegur.Genesis.ContractoServicio.Constantes.NOME_LOG_EVENTOS) Then
                    EventLog.CreateEventSource(Prosegur.Genesis.ContractoServicio.Constantes.NOME_LOG_EVENTOS, Prosegur.Genesis.ContractoServicio.Constantes.NOME_LOG_EVENTOS)
                End If

                ' mensagem de erro
                Dim msgErro As String = "Cod. Erro: " & CodigoErro & Environment.NewLine & " - " & DescricaoErro

                'log the entry
                objEventLog.Source = Prosegur.Genesis.ContractoServicio.Constantes.NOME_LOG_EVENTOS
                objEventLog.WriteEntry(msgErro, EventLogEntryType.Error)

            Catch ex As Exception
                Throw
            End Try

        End Sub

#End Region

#Region "Metodos"

        ''' <summary>
        ''' Serializa um objeto
        ''' </summary>
        ''' <param name="Objeto"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [anselmo.gois] 05/06/2013 - Criado
        ''' </history>
        Public Shared Function Serializar(Objeto As Object) As String

            Dim writer As StringWriter = New StringWriter
            Dim serializer As XmlSerializer = New XmlSerializer(Objeto.GetType())

            serializer.Serialize(writer, Objeto)

            Return writer.ToString()

        End Function

        ''' <summary>
        ''' Mostra Mensagem de Erro na tela
        ''' </summary>
        ''' <param name="erro"></param>
        ''' <remarks></remarks>
        ''' <history>[adans.klevanskis] 05/06/2013 - criado</history>
        Public Shared Function CriarChamadaMensagemErro(erro As String, script As String) As String
            erro = erro.Replace(vbCrLf, "<br />")
            erro = erro.Replace(vbCr, "<br />")
            erro = erro.Replace(vbLf, "<br />")

            Return String.Format("ExibirMensagem('{0}','{1}', '{2}' , '{3}');", _
                                                                    erro, _
                                                                     Tradutor.Traduzir("aplicacao"), script, Tradutor.Traduzir("btnFechar"))
        End Function

        ''' <summary>
        ''' Mostra Mensagem de Erro na tela
        ''' </summary>
        ''' <param name="erro"></param>
        ''' <remarks></remarks>
        ''' <history>[adans.klevanskis] 05/06/2013 - criado</history>
        Public Shared Function CriarChamadaMensagemSimNao(erro As String, acaoSim As String) As String
            erro = erro.Replace(vbCrLf, "<br />")
            erro = erro.Replace(vbCr, "<br />")
            erro = erro.Replace(vbLf, "<br />")

            Return String.Format("ExibirMensagemSimNao('{0}','{1}', '{2}' , '{3}', '{4}');", _
                                                                    erro, _
                                                                     Tradutor.Traduzir("aplicacao"), acaoSim, Tradutor.Traduzir("btnSim"), Tradutor.Traduzir("btnNao"))
        End Function

        ''' <summary>
        ''' Verifica no cache a chave com o identificador, caso não exista o método criará uma chave com o valor passado.
        ''' </summary>
        ''' <param name="chave"></param>
        ''' <param name="id"></param>
        ''' <param name="Value"></param>
        ''' <remarks></remarks>
        Public Shared Sub CriarCachePorIdentificador(chave As String, id As String, Value As Byte())

            If HttpContext.Current.Cache(chave) Is Nothing Then
                HttpContext.Current.Cache(chave) = New Dictionary(Of String, Byte())
            End If

            If HttpContext.Current.Cache(chave).ContainsKey(id) = False Then
                HttpContext.Current.Cache(chave)(id) = Value
            ElseIf chave = "imagen" AndAlso CType(HttpContext.Current.Cache(chave)(id), Byte()).Length <> Value.Length Then
                'Se for icone e o icone que está gravado no cache for diferente do que foi passado, então atualiza o cache
                HttpContext.Current.Cache(chave)(id) = Value
            End If

        End Sub

        ''' <summary>
        ''' Metodo para carga la imagen de la divisa de acuerdo con la imagen en la carpeta Imagenes del NuevoSaldos
        ''' </summary>
        ''' <param name="Divisa"></param>
        ''' [marcel.espiritosanto] Creado 04/10/2013
        ''' <remarks></remarks>
        Public Shared Function CargarImagenDivisa(Divisa As Clases.Divisa, _
                                                  ByRef imgDivisa As Image) As Boolean

            Dim caminoAplicacion As String = AppDomain.CurrentDomain.BaseDirectory
            Dim caminoImagenDivisa As String = "Imagenes\ICO_" & Divisa.CodigoISO & ".png"

            If (File.Exists(caminoAplicacion & caminoImagenDivisa)) Then

                imgDivisa.ImageUrl = "../Imagenes/ICO_" & Divisa.CodigoISO & ".png"
                imgDivisa.ToolTip = Divisa.Descripcion

            Else
                imgDivisa.ImageUrl = "../Imagenes/ICO_NO_DISPONIBLE_30x30.png"
                imgDivisa.ToolTip = Tradutor.Traduzir("012_divisasinimagen")

            End If

            imgDivisa.DataBind()

        End Function

        ''' <summary>
        ''' Verifica se em uma lista de divisas existe alguma repetidas. Agrupando as divisas
        ''' </summary>
        ''' <param name="objListaDivisas"></param>
        ''' <remarks></remarks>
        Public Shared Sub VerificarDivisas(ByRef objListaDivisas As ObservableCollection(Of Clases.Divisa), TipoValor As Genesis.Comon.Enumeradores.TipoValor)
            If objListaDivisas IsNot Nothing Then



                ' Objeto de resposta
                Dim RespDivisas As New ObservableCollection(Of Clases.Divisa)

                ' Percorre todas as divisas da lista
                For Each d As Clases.Divisa In objListaDivisas
                    Dim dLocal = d
                    If RespDivisas Is Nothing OrElse RespDivisas.FindLast(Function(x) x.Identificador = dLocal.Identificador) Is Nothing Then
                        ' Caso o objeto de resposta for vazio ou não conter a divisa em questão, adiciona a divisa.
                        RespDivisas.Add(d.Clonar())
                        Dim objDivisa = RespDivisas.FindLast(Function(x) x.Identificador = dLocal.Identificador)
                        If objDivisa.ValoresTotalesDivisa IsNot Nothing Then
                            objDivisa.ValoresTotalesDivisa.RemoveAll(Function(x) x.TipoValor <> TipoValor)
                        End If
                        If objDivisa.ValoresTotalesEfectivo IsNot Nothing Then
                            objDivisa.ValoresTotalesEfectivo.RemoveAll(Function(x) x.TipoValor <> TipoValor)
                        End If
                        If objDivisa.ValoresTotalesTipoMedioPago IsNot Nothing Then
                            objDivisa.ValoresTotalesTipoMedioPago.RemoveAll(Function(x) x.TipoValor <> TipoValor)
                        End If
                        If objDivisa.Denominaciones IsNot Nothing Then
                            For Each objDenominacion In objDivisa.Denominaciones
                                If objDenominacion IsNot Nothing AndAlso objDenominacion.ValorDenominacion IsNot Nothing Then
                                    objDenominacion.ValorDenominacion.RemoveAll(Function(x) x.TipoValor <> TipoValor)
                                End If
                            Next
                        End If
                        If objDivisa.MediosPago IsNot Nothing Then
                            For Each objMediosPago In objDivisa.MediosPago
                                If objMediosPago IsNot Nothing AndAlso objMediosPago.Valores IsNot Nothing Then
                                    objMediosPago.Valores.RemoveAll(Function(x) x.TipoValor <> TipoValor)
                                End If
                            Next
                        End If

                    Else
                        ' Já existe a divisa, então somamos os Importes
                        Dim objDivisa = RespDivisas.FindLast(Function(x) x.Identificador = dLocal.Identificador)

                        ' ValoresTotalesDivisa
                        If d.ValoresTotalesDivisa IsNot Nothing Then
                            For Each objValor In d.ValoresTotalesDivisa.FindAll(Function(x) x.TipoValor = TipoValor)
                                Dim objValorLocal = objValor
                                If objDivisa.ValoresTotalesDivisa Is Nothing OrElse _
                                    objDivisa.ValoresTotalesDivisa.FindLast(Function(x) x.TipoValor = objValorLocal.TipoValor) Is Nothing Then
                                    If objDivisa.ValoresTotalesDivisa Is Nothing Then
                                        objDivisa.ValoresTotalesDivisa = New ObservableCollection(Of Clases.ValorDivisa)
                                    End If
                                    objDivisa.ValoresTotalesDivisa.Add(objValor)
                                Else
                                    objDivisa.ValoresTotalesDivisa.FindLast(Function(x) x.TipoValor = objValorLocal.TipoValor).Importe += objValor.Importe
                                End If
                            Next
                        End If

                        ' ValoresTotalesEfectivo
                        If d.ValoresTotalesEfectivo IsNot Nothing Then
                            For Each objValor In d.ValoresTotalesEfectivo.FindAll(Function(x) x.TipoValor = TipoValor)
                                Dim objValorLocal = objValor
                                If objDivisa.ValoresTotalesEfectivo Is Nothing OrElse _
                                    objDivisa.ValoresTotalesEfectivo.FindLast(Function(x) x.TipoValor = objValorLocal.TipoValor) Is Nothing Then
                                    If objDivisa.ValoresTotalesEfectivo Is Nothing Then
                                        objDivisa.ValoresTotalesEfectivo = New ObservableCollection(Of Clases.ValorEfectivo)
                                    End If
                                    objDivisa.ValoresTotalesEfectivo.Add(objValor)
                                Else
                                    objDivisa.ValoresTotalesEfectivo.FindLast(Function(x) x.TipoValor = objValorLocal.TipoValor).Importe += objValor.Importe
                                End If
                            Next
                        End If

                        ' ValoresTotalesTipoMedioPago
                        If d.ValoresTotalesTipoMedioPago IsNot Nothing Then
                            For Each objValor In d.ValoresTotalesTipoMedioPago.FindAll(Function(x) x.TipoValor = TipoValor)
                                Dim objValorLocal = objValor
                                If objDivisa.ValoresTotalesTipoMedioPago Is Nothing OrElse _
                                    objDivisa.ValoresTotalesTipoMedioPago.FindLast(Function(x) x.TipoValor = objValorLocal.TipoValor AndAlso x.TipoMedioPago = objValorLocal.TipoMedioPago) Is Nothing Then
                                    If objDivisa.ValoresTotalesTipoMedioPago Is Nothing Then
                                        objDivisa.ValoresTotalesTipoMedioPago = New ObservableCollection(Of Clases.ValorTipoMedioPago)
                                    End If
                                    objDivisa.ValoresTotalesTipoMedioPago.Add(objValor)
                                Else
                                    objDivisa.ValoresTotalesTipoMedioPago.FindLast(Function(x) x.TipoValor = objValorLocal.TipoValor AndAlso x.TipoMedioPago = objValorLocal.TipoMedioPago).Importe += objValor.Importe
                                End If
                            Next
                        End If

                        ' Denominaciones
                        If d.Denominaciones IsNot Nothing Then
                            For Each objDenominacion In d.Denominaciones
                                Dim objDenominacionLocal = objDenominacion
                                If objDivisa.Denominaciones Is Nothing OrElse _
                                    objDivisa.Denominaciones.FindLast(Function(x) x.Identificador = objDenominacionLocal.Identificador) Is Nothing Then
                                    If objDivisa.Denominaciones Is Nothing Then
                                        objDivisa.Denominaciones = New ObservableCollection(Of Clases.Denominacion)
                                    End If
                                    objDivisa.Denominaciones.Add(objDenominacion)
                                ElseIf objDenominacion.ValorDenominacion IsNot Nothing Then
                                    Dim objAux = objDivisa.Denominaciones.FindLast(Function(x) x.Identificador = objDenominacionLocal.Identificador)
                                    For Each objValor In objDenominacion.ValorDenominacion.FindAll(Function(x) x.TipoValor = TipoValor)
                                        Dim objValorLocal = objValor
                                        If objAux.ValorDenominacion Is Nothing OrElse _
                                            objAux.ValorDenominacion.FindLast(Function(x) x.TipoValor = objValorLocal.TipoValor) Is Nothing Then
                                            If objAux.ValorDenominacion Is Nothing Then
                                                objAux.ValorDenominacion = New ObservableCollection(Of Clases.ValorDenominacion)
                                            End If
                                            objAux.ValorDenominacion.Add(objValor)
                                        Else
                                            objAux.ValorDenominacion.FindLast(Function(x) x.TipoValor = objValorLocal.TipoValor).Cantidad += objValor.Cantidad
                                            objAux.ValorDenominacion.FindLast(Function(x) x.TipoValor = objValorLocal.TipoValor).Importe += objValor.Importe
                                        End If
                                    Next
                                End If
                            Next
                        End If

                        ' MediosPago
                        If d.MediosPago IsNot Nothing Then
                            For Each objMediosPago In d.MediosPago
                                Dim objMediosPagoLocal = objMediosPago
                                If objDivisa.MediosPago Is Nothing OrElse _
                                    objDivisa.MediosPago.FindLast(Function(x) x.Identificador = objMediosPagoLocal.Identificador) Is Nothing Then
                                    If objDivisa.MediosPago Is Nothing Then
                                        objDivisa.MediosPago = New ObservableCollection(Of Clases.MedioPago)
                                    End If
                                    objDivisa.MediosPago.Add(objMediosPago)
                                ElseIf objMediosPago.Valores IsNot Nothing Then

                                    Dim objAux = objDivisa.MediosPago.FindLast(Function(x) x.Identificador = objMediosPagoLocal.Identificador)
                                    For Each objValor In objMediosPago.Valores.FindAll(Function(x) x.TipoValor = TipoValor)
                                        Dim objValorLocal = objValor
                                        If objAux.Valores Is Nothing OrElse _
                                            objAux.Valores.FindLast(Function(x) x.TipoValor = objValorLocal.TipoValor) Is Nothing Then
                                            If objAux.Valores Is Nothing Then
                                                objAux.Valores = New ObservableCollection(Of Clases.ValorMedioPago)
                                            End If
                                            objAux.Valores.Add(objValor)
                                        Else
                                            objAux.Valores.FindLast(Function(x) x.TipoValor = objValorLocal.TipoValor).Cantidad += objValor.Cantidad
                                            objAux.Valores.FindLast(Function(x) x.TipoValor = objValorLocal.TipoValor).Importe += objValor.Importe
                                        End If
                                    Next
                                End If
                            Next
                        End If

                        RespDivisas.Remove(RespDivisas.FindLast(Function(x) x.Identificador = dLocal.Identificador))
                        RespDivisas.Add(objDivisa)
                    End If
                Next

                Genesis.Comon.Util.BorrarItemsDivisasSinValoresCantidades(RespDivisas)
                objListaDivisas = RespDivisas
            End If
        End Sub

        ''' <summary>
        ''' Carrega a combo de tipo Contenedor
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Sub CargarComboTipoContenedor(ByRef ddlTipo As DropDownList, Optional esObligatorio As Boolean = False)
            Dim selectedValue As String = ddlTipo.SelectedValue
            ddlTipo.Items.Clear()
            If Not esObligatorio Then
                ddlTipo.Items.Add(New ListItem(Tradutor.Traduzir("gen_selecione"), String.Empty))
            End If
            For Each item In Prosegur.Genesis.LogicaNegocio.Genesis.TipoContenedor.ObtenerTipoContenedorSencillo()
                ddlTipo.Items.Add(New ListItem(item.Descripcion, item.Identificador))
                If item.EsDefecto Then
                    ddlTipo.SelectedValue = item.Identificador
                End If
            Next
            If ddlTipo.Items Is Nothing OrElse ddlTipo.Items.Count = 0 Then
                ddlTipo.Enabled = False
            ElseIf ddlTipo.Items.FindByValue(selectedValue) IsNot Nothing Then
                ddlTipo.SelectedValue = selectedValue
            End If
        End Sub

        ''' <summary>
        ''' Carrega a combo de tipo Formato
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Sub CargarComboTipoFormato(ByRef ddlTipo As DropDownList, Optional esObligatorio As Boolean = False)
            Dim selectedValue As String = ddlTipo.SelectedValue
            ddlTipo.Items.Clear()
            If Not esObligatorio Then
                ddlTipo.Items.Add(New ListItem(Tradutor.Traduzir("gen_selecione"), String.Empty))
            End If
            For Each item In Prosegur.Genesis.LogicaNegocio.Genesis.TipoFormato.ObtenerTiposFormato()
                ddlTipo.Items.Add(New ListItem(item.Descripcion, item.Identificador))
                If item.EsDefecto Then
                    ddlTipo.SelectedValue = item.Identificador
                End If
            Next
            If ddlTipo.Items Is Nothing OrElse ddlTipo.Items.Count = 0 Then
                ddlTipo.Enabled = False
            ElseIf ddlTipo.Items.FindByValue(selectedValue) IsNot Nothing Then
                ddlTipo.SelectedValue = selectedValue
            End If
        End Sub

        ''' <summary>
        ''' Carrega a combo de tipo Servicio
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Sub CargarComboTipoServicio(ByRef ddlTipo As DropDownList, Optional esObligatorio As Boolean = False)
            Dim selectedValue As String = ddlTipo.SelectedValue
            ddlTipo.Items.Clear()
            If Not esObligatorio Then
                ddlTipo.Items.Add(New ListItem(Tradutor.Traduzir("gen_selecione"), String.Empty))
            End If
            For Each item In Prosegur.Genesis.LogicaNegocio.Genesis.TipoServicio.RecuperarTipoServicios()
                ddlTipo.Items.Add(New ListItem(item.Descripcion, item.Identificador))
                If item.EsDefecto Then
                    ddlTipo.SelectedValue = item.Identificador
                End If
            Next
            If ddlTipo.Items Is Nothing OrElse ddlTipo.Items.Count = 0 Then
                ddlTipo.Enabled = False
            ElseIf ddlTipo.Items.FindByValue(selectedValue) IsNot Nothing Then
                ddlTipo.SelectedValue = selectedValue
            End If
        End Sub

        ''' <summary>
        ''' Carrega a combo de tipo Servicio
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Sub CargarComboDivisas(ByRef ddlDivisas As DropDownList, Optional esObligatorio As Boolean = False)
            Dim selectedValue As String = ddlDivisas.SelectedValue
            ddlDivisas.Items.Clear()
            If Not esObligatorio Then
                ddlDivisas.Items.Add(New ListItem(Tradutor.Traduzir("gen_selecione"), String.Empty))
            End If
            For Each item In Prosegur.Genesis.LogicaNegocio.Genesis.Divisas.ObtenerDivisas(Nothing, Nothing, Nothing, True, False, False)
                ddlDivisas.Items.Add(New ListItem(item.Descripcion, item.Identificador))
            Next
            If ddlDivisas.Items Is Nothing OrElse ddlDivisas.Items.Count = 0 Then
                ddlDivisas.Enabled = False
            ElseIf ddlDivisas.Items.FindByValue(selectedValue) IsNot Nothing Then
                ddlDivisas.SelectedValue = selectedValue
            End If
        End Sub

        ''' <summary>
        ''' Agregar divisas distintas entre os componentes de Efectivo e MedioPago
        ''' OBS: Deve ser implementado em todos os chamadores dos controles de divisas efectivo e mediopago
        ''' </summary>
        ''' <param name="Divisas">Lista de divisas</param>
        ''' <param name="DivisasEfectivo">Lista de divisas que pertencem ao componente de Efectivo</param>
        ''' <param name="DivisasMedioPago">Lista de divisas que pertencem ao componente de MedioPago</param>
        ''' <remarks></remarks>
        Public Shared Sub AgregarDivisas(ByRef Divisas As ObservableCollection(Of Clases.Divisa), _
                                         DivisasEfectivo As ObservableCollection(Of Clases.Divisa), _
                                         DivisasMedioPago As ObservableCollection(Of Clases.Divisa))

            If Divisas Is Nothing Then
                Divisas = New ObservableCollection(Of Clases.Divisa)
            Else
                Divisas.Clear()
            End If

            If DivisasEfectivo IsNot Nothing AndAlso DivisasEfectivo.Count > 0 AndAlso
               DivisasMedioPago IsNot Nothing AndAlso DivisasMedioPago.Count > 0 Then

                ' Loop para migrar os medios para as divisas que estão na Lista de Efectivos
                For Each _divisaMedioPago In DivisasMedioPago
                    Dim _divisaMedioPagoLocal = _divisaMedioPago
                    Dim divisa As Clases.Divisa = DivisasEfectivo.Where(Function(d) d.Identificador = _divisaMedioPagoLocal.Identificador).FirstOrDefault

                    If divisa IsNot Nothing Then
                        DivisasEfectivo.Remove(divisa)
                        divisa.MediosPago = _divisaMedioPago.MediosPago
                        divisa.ValoresTotalesTipoMedioPago = _divisaMedioPago.ValoresTotalesTipoMedioPago
                        DivisasEfectivo.Add(divisa)

                    End If

                Next _divisaMedioPago

                ' Recupera as divisas distintas
                Dim DivisasDistintas As ObservableCollection(Of Clases.Divisa) = DivisasMedioPago.Where(Function(dMP) Not DivisasEfectivo.Exists(Function(dEfec) dMP.Identificador = dEfec.Identificador)).ToObservableCollection()

                ' Limpa as denominacoes da lista de divisas de MedioPago que não estão na lista de divisas de Efectivo
                If DivisasDistintas IsNot Nothing AndAlso DivisasDistintas.Count > 0 Then
                    For Each _divisaDistinta In DivisasDistintas
                        _divisaDistinta.Denominaciones = Nothing
                    Next _divisaDistinta
                End If

                ' Agrega as divisas distintas
                DivisasEfectivo.AddRange(DivisasDistintas)

                ' carrega o objeto principal ordenando as divisas por descrição.
                Divisas.AddRange(DivisasEfectivo.OrderBy(Function(f) f.Descripcion))

            ElseIf DivisasEfectivo IsNot Nothing AndAlso DivisasEfectivo.Count > 0 Then
                For Each _divisaEfectivo In DivisasEfectivo
                    _divisaEfectivo.MediosPago = Nothing
                Next _divisaEfectivo
                Divisas.AddRange(DivisasEfectivo.OrderBy(Function(d) d.Descripcion))

            ElseIf DivisasMedioPago IsNot Nothing AndAlso DivisasMedioPago.Count > 0 Then
                For Each _divisaMedioPago In DivisasMedioPago
                    _divisaMedioPago.Denominaciones = Nothing
                Next _divisaMedioPago
                Divisas.AddRange(DivisasMedioPago.OrderBy(Function(d) d.Descripcion))

            Else
                Divisas = Nothing

            End If

        End Sub

        ''' <summary>
        ''' Eliminar divisas sin valores 
        ''' OBS: Deve ser implementado em todos os chamadores dos controles de divisas efectivo e mediopago, onde necessita-se retorno somente
        ''' as divisas que possuem valores
        ''' </summary>
        ''' <param name="Divisas">Lista de divisas</param>
        ''' <remarks></remarks>
        Public Shared Sub EliminarDatosSinValores(ByRef Divisas As ObservableCollection(Of Clases.Divisa))
            Dim Elemento_Divisas As New ObservableCollection(Of Clases.Divisa)
            Elemento_Divisas = If(Divisas IsNot Nothing, Divisas.Clonar, Nothing)

            Dim DivisasRemover As New ObservableCollection(Of Clases.Divisa)

            If Elemento_Divisas IsNot Nothing AndAlso Elemento_Divisas.Count > 0 Then
                For Each _divisa In Elemento_Divisas

                    Dim Denominaciones As New ObservableCollection(Of Clases.Denominacion)
                    If _divisa.Denominaciones IsNot Nothing Then
                        For Each _denominacion In _divisa.Denominaciones
                            If _denominacion.ValorDenominacion IsNot Nothing Then
                                Denominaciones.Add(_denominacion)
                            End If
                        Next _denominacion
                    End If

                    Dim MediosPago As New ObservableCollection(Of Clases.MedioPago)
                    If _divisa.MediosPago IsNot Nothing Then
                        For Each _mediopago In _divisa.MediosPago
                            If _mediopago.Valores IsNot Nothing Then
                                MediosPago.Add(_mediopago)
                            End If
                        Next _mediopago
                    End If

                    _divisa.Denominaciones = Denominaciones
                    _divisa.MediosPago = MediosPago

                    Dim BolDivisaRemover As Boolean = False

                    If (_divisa.Denominaciones Is Nothing) OrElse (_divisa.Denominaciones IsNot Nothing AndAlso _divisa.Denominaciones.Count = 0) Then
                        If (_divisa.MediosPago Is Nothing) OrElse ((_divisa.MediosPago IsNot Nothing AndAlso _divisa.MediosPago.Count = 0)) Then
                            If _divisa.ValoresTotalesDivisa Is Nothing AndAlso _divisa.ValoresTotalesEfectivo Is Nothing AndAlso _divisa.ValoresTotalesTipoMedioPago Is Nothing Then
                                BolDivisaRemover = True
                            End If
                        End If
                    End If

                    If BolDivisaRemover Then
                        DivisasRemover.Add(_divisa)
                    End If

                Next _divisa
                Divisas = Elemento_Divisas
                RemoverDivisasSinValor(Divisas, DivisasRemover)
            End If

        End Sub
        ''' <summary>
        ''' Eliminar da lista todas las divisas que no tiene ningún valor.
        ''' </summary>
        ''' <param name="Divisas">Lista de divisas</param>
        ''' <param name="DivisasRemover">Lista de divisas a seren borradas</param>
        ''' <remarks></remarks>
        Private Shared Sub RemoverDivisasSinValor(ByRef Divisas As ObservableCollection(Of Clases.Divisa), _
                                                  DivisasRemover As ObservableCollection(Of Clases.Divisa))

            If DivisasRemover IsNot Nothing AndAlso DivisasRemover.Count > 0 Then
                If Divisas.Count = DivisasRemover.Count Then
                    Divisas = Nothing
                Else
                    For Each objDivisa In DivisasRemover
                        Dim objDivisaLocal = objDivisa
                        Divisas.RemoveAll(Function(d) d.Identificador = objDivisaLocal.Identificador)
                    Next
                End If
            End If

        End Sub
        ''' <summary>
        ''' Agrega en la lista de divisas las denominaciones y medios pagos que no tiene valores. 
        ''' OBS: Deve ser implementado somente em modo de modificação.
        ''' </summary>
        ''' <param name="Divisas">Lista de divisas</param>
        ''' <param name="EfectivoMedioPago">Define si está trabalhando com efectivo o mediopago</param>
        ''' <remarks></remarks>
        Public Shared Sub AgregarDependenciasEnDivisa(ByRef Divisas As ObservableCollection(Of Clases.Divisa), EfectivoMedioPago As Char)

            If Divisas IsNot Nothing AndAlso Divisas.Count > 0 Then
                Dim DenominacionesExtras As New ObservableCollection(Of Clases.Denominacion)
                Dim MediosPagoExtras As New ObservableCollection(Of Clases.MedioPago)
                For Each _divisa In Divisas

                    If EfectivoMedioPago = "E" Then

                        Dim Denominaciones As New ObservableCollection(Of Clases.Denominacion)
                        Dim ListaIdentificadorDenominacion As New ObservableCollection(Of String)

                        If _divisa.Denominaciones IsNot Nothing Then
                            For Each _denominacion In _divisa.Denominaciones
                                If _denominacion.ValorDenominacion IsNot Nothing Then
                                    Denominaciones.Add(_denominacion)
                                    ListaIdentificadorDenominacion.Add(_denominacion.Identificador)
                                End If
                            Next _denominacion
                            _divisa.Denominaciones.Clear()
                            DenominacionesExtras = Prosegur.Genesis.LogicaNegocio.Genesis.Denominacion.ObtenerDenominaciones(_divisa.Identificador, ListaIdentificadorDenominacion, True)
                        Else
                            DenominacionesExtras = Prosegur.Genesis.LogicaNegocio.Genesis.Denominacion.ObtenerDenominaciones(_divisa.Identificador, Nothing)
                        End If

                        If Denominaciones.Count > 0 Then
                            _divisa.Denominaciones.AddRange(Denominaciones)
                        End If
                        If DenominacionesExtras IsNot Nothing AndAlso DenominacionesExtras.Count > 0 Then
                            If _divisa.Denominaciones Is Nothing Then
                                _divisa.Denominaciones = New ObservableCollection(Of Clases.Denominacion)(DenominacionesExtras)
                            Else
                                _divisa.Denominaciones.AddRange(DenominacionesExtras)
                            End If

                        End If

                    Else

                        Dim MediosPago As New ObservableCollection(Of Clases.MedioPago)
                        Dim ListaIdentificadorMedioPago As New List(Of String)
                        If _divisa.MediosPago IsNot Nothing Then
                            For Each _mediopago In _divisa.MediosPago
                                If _mediopago.Valores IsNot Nothing Then
                                    MediosPago.Add(_mediopago)
                                    ListaIdentificadorMedioPago.Add(_mediopago.Identificador)
                                End If
                            Next _mediopago
                            _divisa.MediosPago.Clear()
                            MediosPagoExtras = Prosegur.Genesis.LogicaNegocio.Genesis.MedioPago.ObtenerMediosPago(_divisa.Identificador, ListaIdentificadorMedioPago, True)
                        Else
                            MediosPagoExtras = Prosegur.Genesis.LogicaNegocio.Genesis.MedioPago.ObtenerMediosPago(_divisa.Identificador, Nothing)
                        End If

                        If MediosPago.Count > 0 Then
                            _divisa.MediosPago.AddRange(MediosPago)
                        End If
                        If MediosPagoExtras IsNot Nothing AndAlso MediosPagoExtras.Count > 0 Then
                            If _divisa.MediosPago Is Nothing Then
                                _divisa.MediosPago = New ObservableCollection(Of Clases.MedioPago)(MediosPagoExtras)
                            Else
                                _divisa.MediosPago.AddRange(MediosPagoExtras)
                            End If

                        End If

                    End If

                Next _divisa
                OrdenarItemsDivisas(Divisas)
            End If

        End Sub

        Public Shared Sub OrdenarItemsDivisas(ByRef Divisas As ObservableCollection(Of Clases.Divisa))

            For Each _divisa In Divisas
                If _divisa.Denominaciones IsNot Nothing AndAlso _divisa.Denominaciones.Count > 0 Then

                    Dim denominaciones = (From den In _divisa.Denominaciones
                                      Order By den.EsBillete Descending, den.Valor Descending
                                      Select den)

                    _divisa.Denominaciones = denominaciones

                End If

                If _divisa.MediosPago IsNot Nothing AndAlso _divisa.MediosPago.Count > 0 Then

                    Dim mediosPago = (From mp In _divisa.MediosPago
                                      Order By mp.Codigo, mp.Descripcion
                                      Select mp)

                    _divisa.MediosPago = mediosPago

                End If

            Next _divisa

        End Sub

        ''' <summary>
        ''' Añadir script de mascara para campos de Importe y Cantidad
        ''' </summary>
        ''' <param name="Controle"></param>
        ''' <param name="DecimalSeparador"></param>
        ''' <param name="MilharSeparador"></param>
        ''' <param name="ImporteCantidad"></param>
        ''' <remarks></remarks>
        Public Shared Sub CargarScripts(ByRef Controle As System.Web.UI.WebControls.TextBox, _
                                        DecimalSeparador As String, _
                                        MilharSeparador As String, _
                                        ImporteCantidad As Char, _
                                        Optional AceitaNegativo As Boolean = False)

            If ImporteCantidad = "I" Then
                If AceitaNegativo Then
                    Controle.Attributes.Add("onkeypress", "return bloqueialetrasImporteNegativo(event,this);")
                Else
                    Controle.Attributes.Add("onkeypress", "return bloqueialetrasImporte(event,this);")
                End If
                Controle.Attributes.Add("onpaste", "return false;")
                Controle.Attributes.Add("onkeyup", String.Format("moedaIAC(event,this,'{0}','{1}');", DecimalSeparador, MilharSeparador))
            Else
                If AceitaNegativo Then
                    Controle.Attributes.Add("onkeypress", "return bloqueialetrasAceitaNegativo(event,this);")
                Else
                    Controle.Attributes.Add("onkeypress", "return bloqueialetras(event,this);")
                End If
                Controle.Attributes.Add("onpaste", "return false;")
                Controle.Attributes.Add("onkeydown", "BloquearColar();")
            End If

        End Sub

        ' ''' <summary>
        ' ''' Método que configura o tabindex dos campos, o tabindex é definido pela ordem que os controles são passados.
        ' ''' </summary>
        ' ''' <param name="controles">Controles que terão seu tabindex configurados.</param>
        ' ''' <remarks></remarks>
        'Public Shared Sub ConfigurarTabIndex(ParamArray controles As System.Web.UI.Control())
        '    For Each controle As System.Web.UI.Control In controles
        '        If (controle Is Nothing) Then
        '            Continue For
        '        End If

        '        If (TypeOf controle Is UcBase) Then
        '            DirectCast(controle, UcBase).ConfigurarTabIndexControle()
        '        ElseIf (TypeOf controle Is WebControl) Then
        '            Dim controleWeb As WebControl = DirectCast(controle, WebControl)
        '            controleWeb.TabIndex = Aplicacao.Util.Utilidad.TabIndex

        '            Aplicacao.Util.Utilidad.TabIndex += 1
        '        End If
        '    Next
        'End Sub

        Public Shared Sub ZerarTabIndex()
            TabIndex = 1
        End Sub

        ''' <summary>
        ''' Configura uma string para exibir os dados da Cuenta
        ''' </summary>
        ''' <param name="objCuenta"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function PreencheCuenta(objCuenta As Clases.Cuenta, Optional sectorVisible As Boolean = False) As String
            Dim objRespuesta As String = ""

            If objCuenta IsNot Nothing Then

                If sectorVisible Then
                    If objCuenta.Sector IsNot Nothing AndAlso Not String.IsNullOrEmpty(objCuenta.Sector.Codigo) AndAlso Not String.IsNullOrEmpty(objCuenta.Sector.Descripcion) Then
                        objRespuesta &= "<strong>" & objCuenta.Sector.Codigo & " - " & objCuenta.Sector.Descripcion & "</strong>"
                    End If
                End If

                If objCuenta.Canal IsNot Nothing AndAlso Not String.IsNullOrEmpty(objCuenta.Canal.Descripcion) Then
                    If Not String.IsNullOrEmpty(objRespuesta) Then
                        objRespuesta &= " | "
                    End If
                    objRespuesta &= objCuenta.Canal.Codigo & " - " & objCuenta.Canal.Descripcion
                End If
                If objCuenta.SubCanal IsNot Nothing AndAlso Not String.IsNullOrEmpty(objCuenta.SubCanal.Descripcion) Then
                    If Not String.IsNullOrEmpty(objRespuesta) Then
                        objRespuesta &= " | "
                    End If
                    objRespuesta &= objCuenta.SubCanal.Codigo & " - " & objCuenta.SubCanal.Descripcion
                End If

                If objCuenta.Cliente IsNot Nothing AndAlso Not String.IsNullOrEmpty(objCuenta.Cliente.Descripcion) Then
                    If Not String.IsNullOrEmpty(objRespuesta) Then
                        objRespuesta &= " | "
                    End If
                    objRespuesta &= objCuenta.Cliente.Codigo & " - " & objCuenta.Cliente.Descripcion
                End If

                If objCuenta.SubCliente IsNot Nothing AndAlso Not String.IsNullOrEmpty(objCuenta.SubCliente.Descripcion) Then
                    If Not String.IsNullOrEmpty(objRespuesta) Then
                        objRespuesta &= " | "
                    End If
                    objRespuesta &= objCuenta.SubCliente.Codigo & " - " & objCuenta.SubCliente.Descripcion
                End If
                If objCuenta.PuntoServicio IsNot Nothing AndAlso Not String.IsNullOrEmpty(objCuenta.PuntoServicio.Descripcion) Then
                    If Not String.IsNullOrEmpty(objRespuesta) Then
                        objRespuesta &= " | "
                    End If
                    objRespuesta &= objCuenta.PuntoServicio.Codigo & " - " & objCuenta.PuntoServicio.Descripcion
                End If


            End If

            Return objRespuesta
        End Function

        ''' <summary>
        ''' Configura uma string para exibir os dados da Cuenta
        ''' </summary>
        ''' <param name="objCuenta"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function PreencheCanal(objCuenta As Clases.Cuenta) As String
            Dim objRespuesta As String = ""

            If objCuenta IsNot Nothing Then

                If objCuenta.Canal IsNot Nothing AndAlso Not String.IsNullOrEmpty(objCuenta.Canal.Descripcion) Then
                    If Not String.IsNullOrEmpty(objRespuesta) Then
                        objRespuesta &= " | "
                    End If
                    objRespuesta &= objCuenta.Canal.Codigo & " - " & objCuenta.Canal.Descripcion
                End If
                If objCuenta.SubCanal IsNot Nothing AndAlso Not String.IsNullOrEmpty(objCuenta.SubCanal.Descripcion) Then
                    If Not String.IsNullOrEmpty(objRespuesta) Then
                        objRespuesta &= " | "
                    End If
                    objRespuesta &= objCuenta.SubCanal.Codigo & " - " & objCuenta.SubCanal.Descripcion
                End If

            End If

            Return objRespuesta
        End Function

        ''' <summary>
        ''' Configura uma string para exibir os dados da Cuenta
        ''' </summary>
        ''' <param name="objCuenta"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function PreencheCliente(objCuenta As Clases.Cuenta) As String
            Dim objRespuesta As String = ""

            If objCuenta IsNot Nothing Then

                If objCuenta.Cliente IsNot Nothing AndAlso Not String.IsNullOrEmpty(objCuenta.Cliente.Descripcion) Then
                    If Not String.IsNullOrEmpty(objRespuesta) Then
                        objRespuesta &= " | "
                    End If
                    objRespuesta &= objCuenta.Cliente.Codigo & " - " & objCuenta.Cliente.Descripcion
                End If

                If objCuenta.SubCliente IsNot Nothing AndAlso Not String.IsNullOrEmpty(objCuenta.SubCliente.Descripcion) Then
                    If Not String.IsNullOrEmpty(objRespuesta) Then
                        objRespuesta &= " | "
                    End If
                    objRespuesta &= objCuenta.SubCliente.Codigo & " - " & objCuenta.SubCliente.Descripcion
                End If
                If objCuenta.PuntoServicio IsNot Nothing AndAlso Not String.IsNullOrEmpty(objCuenta.PuntoServicio.Descripcion) Then
                    If Not String.IsNullOrEmpty(objRespuesta) Then
                        objRespuesta &= " | "
                    End If
                    objRespuesta &= objCuenta.PuntoServicio.Codigo & " - " & objCuenta.PuntoServicio.Descripcion
                End If


            End If

            Return objRespuesta
        End Function

        ''' <summary>
        ''' Configura uma string para exibir os dados da Cuenta (Lista de Valores)
        ''' </summary>
        ''' <param name="objCuenta"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function PreencheCuentaValores(objCuenta As Clases.Cuenta) As String
            Dim objRespuesta As String = ""

            If objCuenta IsNot Nothing Then
                If objCuenta.SubCanal IsNot Nothing AndAlso Not String.IsNullOrEmpty(objCuenta.SubCanal.Descripcion) Then
                    objRespuesta &= objCuenta.SubCanal.Codigo & " - " & objCuenta.SubCanal.Descripcion
                End If

                If objCuenta.Cliente IsNot Nothing AndAlso Not String.IsNullOrEmpty(objCuenta.Cliente.Descripcion) Then
                    If Not String.IsNullOrEmpty(objRespuesta) Then
                        objRespuesta &= " | "
                    End If
                    objRespuesta &= objCuenta.Cliente.Codigo & " - " & objCuenta.Cliente.Descripcion
                End If
                If objCuenta.Sector IsNot Nothing AndAlso Not String.IsNullOrEmpty(objCuenta.Sector.Descripcion) Then
                    If Not String.IsNullOrEmpty(objRespuesta) Then
                        objRespuesta &= " | "
                    End If
                    objRespuesta &= objCuenta.Sector.Codigo & " - " & objCuenta.Sector.Descripcion
                End If
            End If
            Return objRespuesta
        End Function

#End Region

#Region "Shareds"
        Private Shared Property TabIndex As Short


        'Número máximos de registros a serem exibidos no GridView
        Public Const MaximoRegistrosPageGrid As Integer = 10
        ''' <summary>
        ''' Define o número máximo de caracteres a serem exibidos
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function getMaximoRegistrosPageGrid(Optional valorSession As Integer = 0) As Integer
            Try
                Dim intMaximoRegistroGrid As Integer = Prosegur.Genesis.Web.Login.Configuraciones.AppSettings("MaximoRegistrosPageGrid")

                If intMaximoRegistroGrid > 0 Then
                    Return intMaximoRegistroGrid
                Else
                    If valorSession > 0 Then
                        Return valorSession
                    End If
                    Return MaximoRegistrosPageGrid
                End If

            Catch ex As Exception
                Return MaximoRegistrosPageGrid
            End Try
        End Function

        Public Shared Function RecuperarParametrosReporte(CodigoDelegacion As String) As Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio.GetParametrosDelegacionPais.Respuesta

            Dim proxyIAC As New Prosegur.Genesis.Comunicacion.ProxyIacIntegracion()
            Dim parametrosReportServices As Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio.GetParametrosDelegacionPais.Respuesta = Nothing
            Dim peticionParametrosIAC As New Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio.GetParametrosDelegacionPais.Peticion()
            peticionParametrosIAC.CodigoAplicacion = Prosegur.Genesis.Comon.Constantes.CODIGO_APLICACION_GENESIS_REPORTES
            peticionParametrosIAC.CodigoDelegacion = CodigoDelegacion
            peticionParametrosIAC.Parametros = New Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio.GetParametrosDelegacionPais.ParametroColeccion()
            peticionParametrosIAC.Parametros.Add(New Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio.GetParametrosDelegacionPais.Parametro() With {.CodigoParametro = Prosegur.Genesis.ContractoServicio.Constantes.CONST_COD_PARAMETRO_REPORTES_URL})
            peticionParametrosIAC.Parametros.Add(New Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio.GetParametrosDelegacionPais.Parametro() With {.CodigoParametro = Prosegur.Genesis.ContractoServicio.Constantes.CONST_COD_PARAMETRO_REPORTES_USER})
            peticionParametrosIAC.Parametros.Add(New Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio.GetParametrosDelegacionPais.Parametro() With {.CodigoParametro = Prosegur.Genesis.ContractoServicio.Constantes.CONST_COD_PARAMETRO_REPORTES_PASS})
            peticionParametrosIAC.Parametros.Add(New Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio.GetParametrosDelegacionPais.Parametro() With {.CodigoParametro = Prosegur.Genesis.ContractoServicio.Constantes.CONST_COD_PARAMETRO_REPORTES_DOMAIN})
            peticionParametrosIAC.Parametros.Add(New Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio.GetParametrosDelegacionPais.Parametro() With {.CodigoParametro = Prosegur.Genesis.ContractoServicio.Constantes.CONST_COD_PARAMETRO_REPORTES_CARPETA_REPORTES})

            parametrosReportServices = proxyIAC.GetParametrosDelegacionPais(peticionParametrosIAC)
            Dim parametroConfigurado As Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio.GetParametrosDelegacionPais.ParametroRespuesta = Nothing

            parametroConfigurado = parametrosReportServices.Parametros.Find(Function(p) p.CodigoParametro = Prosegur.Genesis.ContractoServicio.Constantes.CONST_COD_PARAMETRO_REPORTES_URL)
            If parametroConfigurado Is Nothing OrElse String.IsNullOrWhiteSpace(parametroConfigurado.ValorParametro) Then
                Throw New Prosegur.Genesis.Excepcion.NegocioExcepcion(Prosegur.Genesis.Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Tradutor.Traduzir("app_ParametrosReportServiceNoInformados"), Prosegur.Genesis.ContractoServicio.Constantes.CONST_COD_PARAMETRO_REPORTES_URL))
            End If

            parametroConfigurado = parametrosReportServices.Parametros.Find(Function(p) p.CodigoParametro = Prosegur.Genesis.ContractoServicio.Constantes.CONST_COD_PARAMETRO_REPORTES_CARPETA_REPORTES)
            If parametroConfigurado Is Nothing OrElse String.IsNullOrWhiteSpace(parametroConfigurado.ValorParametro) Then
                Throw New Prosegur.Genesis.Excepcion.NegocioExcepcion(Prosegur.Genesis.Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Tradutor.Traduzir("app_ParametrosReportServiceNoInformados"), Prosegur.Genesis.ContractoServicio.Constantes.CONST_COD_PARAMETRO_REPORTES_CARPETA_REPORTES))
            End If

            parametroConfigurado = parametrosReportServices.Parametros.Find(Function(p) p.CodigoParametro = Prosegur.Genesis.ContractoServicio.Constantes.CONST_COD_PARAMETRO_REPORTES_USER)
            If parametroConfigurado Is Nothing OrElse String.IsNullOrWhiteSpace(parametroConfigurado.ValorParametro) Then
                Throw New Prosegur.Genesis.Excepcion.NegocioExcepcion(Prosegur.Genesis.Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Tradutor.Traduzir("app_ParametrosReportServiceNoInformados"), Prosegur.Genesis.ContractoServicio.Constantes.CONST_COD_PARAMETRO_REPORTES_USER))
            End If

            parametroConfigurado = parametrosReportServices.Parametros.Find(Function(p) p.CodigoParametro = Prosegur.Genesis.ContractoServicio.Constantes.CONST_COD_PARAMETRO_REPORTES_PASS)
            If parametroConfigurado Is Nothing OrElse String.IsNullOrWhiteSpace(parametroConfigurado.ValorParametro) Then
                Throw New Prosegur.Genesis.Excepcion.NegocioExcepcion(Prosegur.Genesis.Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Tradutor.Traduzir("app_ParametrosReportServiceNoInformados"), Prosegur.Genesis.ContractoServicio.Constantes.CONST_COD_PARAMETRO_REPORTES_PASS))
            End If

            parametroConfigurado = parametrosReportServices.Parametros.Find(Function(p) p.CodigoParametro = Prosegur.Genesis.ContractoServicio.Constantes.CONST_COD_PARAMETRO_REPORTES_DOMAIN)
            If parametroConfigurado Is Nothing OrElse String.IsNullOrWhiteSpace(parametroConfigurado.ValorParametro) Then
                Throw New Prosegur.Genesis.Excepcion.NegocioExcepcion(Prosegur.Genesis.Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Tradutor.Traduzir("app_ParametrosReportServiceNoInformados"), Prosegur.Genesis.ContractoServicio.Constantes.CONST_COD_PARAMETRO_REPORTES_DOMAIN))
            End If


            Return parametrosReportServices

        End Function
#End Region
    End Class
End Namespace