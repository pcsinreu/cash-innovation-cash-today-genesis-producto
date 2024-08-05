Imports Prosegur.Global.GesEfectivo.IAC
Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio
Imports System.Text
Imports System.Text.RegularExpressions
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis
Imports Prosegur.Genesis.Comunicacion
Imports System.IO
Imports System.Xml.Serialization

Namespace Aplicacao.Util

    Public Class Utilidad

#Region "Constantes"

        'Mensagens 
        Public Const InfoMsgBaja As String = "info_msg_baja"
        Public Const InfoMsgSeleccionarRegistro As String = "info_msg_seleccionar_registro"
        Public Const InfoMsgSeleccionarCliente As String = "info_msg_seleccionar_cliente"
        Public Const InfoMsgBajaRegistro As String = "info_msg_baja_registro"
        Public Const InfoMsgGrabarRegistro As String = "info_msg_grabar"
        Public Const InfoMsgAltaRegistro As String = "info_msg_alta"
        Public Const InfoMsgSinRegistro As String = "info_msg_sin_registro"
        Public Const InfoMsgMaxRegistro As String = "info_msg_max_registro"
        Public Const InfoMsgSairPagina As String = "info_msg_sair_pagina"
        Public Const GenOpcionSi As String = "gen_opcion_si"
        Public Const GenOpcionNo As String = "gen_opcion_no"
        'Número máximos de caracteres a serem exibidos por coluna no GridView
        Public Const MaximoCaracteresLinha As Integer = 20
        'Número máximos de registros a serem exibidos no GridView
        Public Const MaximoRegistrosGrid As Integer = 100
        'Quebra de linha
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
            Erro = 7
            Duplicar = 8
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
            ERRO
            LOGIN
            MENU
            CLIENTES
            SUB_CLIENTES
            CANALES
            PROCEDENCIAS
            MODALIDAD_RECUENTO
            PRODUCTOS
            DIVISAS
            DELEGACION
            DIRECCION
            MEDIO_DE_PAGO
            AGRUPACIONES
            TERMINOS_IAC
            INFORMACION_ADICIONAL_CLIENTE
            PLANTAS
            PROCESOS
            INTEGRACION_IAC_BO
            INTEGRACION_BO
            TIPO_PROCESADO
            TIPOSECTOR
            TIPOCLIENTE
            TIPOPROCEDENCIA
            TIPOPUNTOSERVICIO
            TIPOSUBCLIENTE
            VALORES_POSIBLES
            PUNTO_SERVICIO
            TOLERANCIAS
            CARACTERISTICAS
            MORFOLOGIA
            NIVELESSALDOS
            ATM
            PUESTOS
            CONFIGURACION_PARAMETRO
            PARAMETRO
            GRUPO_CLIENTE
            SECTOR
            CODIGO_AJENO
            IMPORTEMAXIMO
            MAE
            PLANIFICACION
            ACCIONESLOTE
            APROBACION_CUENTAS_BANCARIAS
            PERIODOS_ACREDITACION
            PANTALLA_PERIODOS
            CONFIGURACION_ROLES
            CONFIGURACION_USUARIOS
            MENSAJES
            ORDENES_SERVICIO
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
            _DUPLICAR
            _DAR_BAJA
            _MODIFICAR
            _MODIFICACION
            _ENVIAR_DATOS
            _GESTIONAR_VALORES_TERMINO
            _ASIGNAR_FECHAS
            _MODIFICAR_FECHAS
            _SINCRONIZAR
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

        Public Shared Function CriarChamadaMensagemErro(erro As String, script As String) As String
            erro = erro.Replace(vbCrLf, "<br />")
            erro = erro.Replace(vbCr, "<br />")
            erro = erro.Replace(vbLf, "<br />")

            Return String.Format("ExibirMensagem('{0}','{1}', '{2}' , '{3}');", _
                                                                    erro, _
                                                                     Traduzir("aplicacao"), script, Traduzir("btnFechar"))
        End Function
        ''' <summary>
        ''' Faz o tratamento do retorno das mensagens de erro.
        ''' </summary>
        ''' <param name="msgErro"></param>
        ''' <param name="msgErroExibicao"></param>
        ''' <param name="nomeServidor"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [anselmo.gois] 21/09/2010 - Criado
        ''' </history>
        Public Shared Function RetornarMensagemErro(msgErro As String, msgErroExibicao As String, _
                                                    Optional nomeServidor As String = "", _
                                                    Optional objTraduzir As Boolean = False) As String

            If Not String.IsNullOrEmpty(msgErro) Then

                Dim palavras() As String = msgErro.Split(" ")

                If (From p In palavras Where p.Trim.ToLower = Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Constantes.CONST_ERRO_ACCESO_LDAP.ToLower).Count > 0 Then

                    'Retorna erro de acesso ao servidor de ad
                    Return If(objTraduzir, Traduzir("err_msg_error_03"), "err_msg_error_03")

                ElseIf (From p In palavras Where p.Trim.ToLower = Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Constantes.CONST_ERRO_BANCO.ToLower OrElse _
                                                 p.Trim.ToLower = Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Constantes.CONST_ERRO_BANCO2.ToLower).Count > 0 Then

                    'Retorna erro de banco de dados.
                    Return If(objTraduzir, String.Format(Traduzir("err_msg_error_05"), nomeServidor), String.Format("err_msg_error_05", nomeServidor))

                ElseIf (From p In palavras Where p.Trim.ToLower = Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Constantes.CONST_ERRO_404B.ToLower _
                        OrElse p.Trim.ToLower = Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Constantes.CONST_ERRO_404A.ToLower _
                        OrElse p.Trim.ToLower = Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Constantes.CONST_ERRO_DNS.ToLower _
                        OrElse p.Trim.ToLower = Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Constantes.CONST_ERRO_URL.ToLower _
                        OrElse p.Trim.ToLower = Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Constantes.CONST_ERRO_407B.ToLower _
                        OrElse p.Trim.ToLower = Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Constantes.CONST_ERRO_407A.ToLower _
                        OrElse p.Trim.ToLower = Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Constantes.CONST_ERRO_COMUNICACION_LDAP.ToLower).Count > 0 Then

                    'Retorna erro de url invalida
                    Return If(objTraduzir, Traduzir("err_msg_error_02"), "err_msg_error_02")

                End If

            End If

            Return msgErroExibicao
        End Function

        ''' <summary>
        ''' Trata o retorno da chamada de um serviço
        ''' </summary>
        ''' <param name="CodErro"></param>
        ''' <param name="MsgError"></param>
        ''' <param name="MsgRetorno"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [octavio.piramo] 04/02/2009 Criado
        ''' </history>
        Public Shared Function TratarRetornoServico(CodErro As Integer, _
                                                    MsgError As String, _
                                                    Optional ByRef MsgRetorno As String = "", _
                                                    Optional ByRef objTraduzir As Boolean = True, _
                                                    Optional LoginUsuario As String = "", _
                                                    Optional DelegacionUsuario As String = "") As Boolean

            Select Case CodErro

                Case Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
                    Return True

                Case Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
                    MsgRetorno = Traduzir("err_codigo_erro_ambiente_default")

                    ' logar erro no banco
                    Aplicacao.Util.Utilidad.LogarErroAplicacao(CodErro, MsgError, String.Empty, LoginUsuario, DelegacionUsuario)

                Case (Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT + 1) To Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT
                    'Caso a mensagem de erro do negócio não tenha sido informada, então exibe a padrão de negócio.
                    If MsgError <> String.Empty Then
                        MsgRetorno = MsgError
                    Else
                        MsgRetorno = Traduzir("err_codigo_erro_negocio_default")
                    End If

                Case Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_SESION_INVALIDA
                    MsgRetorno = Traduzir("err_codigo_erro_negocio_sesion_invalida")

                Case Else
                    MsgRetorno = Traduzir("err_padrao_aplicacao")

            End Select

            objTraduzir = False
            Return False

        End Function

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

                Dim Peticion As New ContractoServicio.Log.Peticion
                Dim RespuestaLog As ContractoServicio.Log.Respuesta
                Peticion.FYHErro = DateTime.Now

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
                If Not EventLog.SourceExists(Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Constantes.NOME_LOG_EVENTOS) Then
                    EventLog.CreateEventSource(Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Constantes.NOME_LOG_EVENTOS, Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Constantes.NOME_LOG_EVENTOS)
                End If

                ' mensagem de erro
                Dim msgErro As String = "Cod. Erro: " & CodigoErro & Environment.NewLine & " - " & DescricaoErro

                'log the entry
                objEventLog.Source = Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Constantes.NOME_LOG_EVENTOS
                objEventLog.WriteEntry(msgErro, EventLogEntryType.Error)

            Catch ex As Exception
            End Try

        End Sub

#End Region

#Region "Metodos"

        ''' <summary>
        ''' Responsavel por ir no DB e buscar as informações para preencher o dropdownlist Nível.
        ''' </summary>
        Public Shared Function getComboNivelesParametros(Permisos As List(Of String)) As ContractoServicio.Utilidad.GetComboNivelesParametros.NivelParametroColeccion

            Dim objProxyUtilidad As New Comunicacion.ProxyUtilidad
            Dim objPeticion As New IAC.ContractoServicio.Utilidad.GetComboNivelesParametros.Peticion
            Dim objRespuesta As IAC.ContractoServicio.Utilidad.GetComboNivelesParametros.Respuesta

            'Recebe os valores do filtro        
            'objPeticion.Permisos = InformacionUsuario.Permisos

            objRespuesta = objProxyUtilidad.GetComboNivelesParametros(objPeticion)

            If objRespuesta.CodigoError <> Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT Then
                Throw New Exception(objRespuesta.MensajeError)
            End If

            Dim ObjNiveles As New ContractoServicio.Utilidad.GetComboNivelesParametros.NivelParametroColeccion

            For Each nivel In objRespuesta.NivelesParametros
                If Permisos.FirstOrDefault(Function(x) x.Contains(nivel.CodigoPermiso)) IsNot Nothing Then
                    ObjNiveles.Add(nivel)
                End If
            Next

            Return ObjNiveles
        End Function

        ''' <summary>
        ''' Responsavel por ir no DB e buscar as informações para preencher o dropdownlist aplicação.
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Function getComboAplicaciones(Permisos As List(Of String)) As List(Of Aplicacion)
            Dim objProxyUtilidad As New ProxyUtilidad
            Dim objRespuestaServicio As New IAC.ContractoServicio.Utilidad.getComboAplicaciones.Respuesta
            objRespuestaServicio = objProxyUtilidad.GetComboAplicaciones()

            If objRespuestaServicio.CodigoError <> Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT Then
                Throw New Exception(objRespuestaServicio.MensajeError)
            End If

            Return ConvertToList(Permisos, objRespuestaServicio.Aplicaciones)

        End Function

        Public Shared Function ConvertToList(Permisos As List(Of String), Aplicaciones As ContractoServicio.Utilidad.getComboAplicaciones.AplicacionColeccion) As List(Of Aplicacion)

            Dim lista As New List(Of Aplicacion)
            Dim objAplicacion As Aplicacion

            If Aplicaciones Is Nothing Then
                Return lista
            End If

            For Each item In Aplicaciones

                If Permisos.FirstOrDefault(Function(x) x.Contains(item.CodigoPermiso)) IsNot Nothing Then
                    objAplicacion = New Aplicacion

                    With objAplicacion
                        .CodigoAplicacion = item.CodigoAplicacion
                        .DescripcionAplicacion = item.DescripcionAplicacion
                        .CodigoPermiso = item.CodigoPermiso

                    End With

                    lista.Add(objAplicacion)
                End If

            Next

            Return lista

        End Function

        ''' <summary>
        ''' Define o número máximo de caracteres a serem exibidos
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function getMaximoCaracteresLinha() As Integer
            Try
                Dim intMaximoCaraterLinha As Integer = Prosegur.Genesis.Web.Login.Configuraciones.AppSettings("MaximoCaracteresLinha")

                If intMaximoCaraterLinha > 0 Then
                    Return intMaximoCaraterLinha
                Else
                    Return MaximoCaracteresLinha
                End If

            Catch ex As Exception
                Return MaximoCaracteresLinha
            End Try
        End Function

        ''' <summary>
        ''' Define o número máximo de caracteres a serem exibidos
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function getMaximoRegistroGrid() As Integer
            Try
                Dim intMaximoRegistroGrid As Integer = Prosegur.Genesis.Web.Login.Configuraciones.AppSettings("MaximoRegistroGrid")

                If intMaximoRegistroGrid > 0 Then
                    Return intMaximoRegistroGrid
                Else
                    Return MaximoRegistrosGrid
                End If

            Catch ex As Exception
                Return MaximoRegistrosGrid
            End Try
        End Function

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="CurrentControl"></param>
        ''' <remarks></remarks>
        Public Shared Sub HookOnFocus(CurrentControl As Control)
            If CurrentControl.GetType() Is GetType(TextBox) OrElse CurrentControl.GetType() Is GetType(DropDownList) OrElse _
            CurrentControl.GetType() Is GetType(ListBox) OrElse CurrentControl.GetType() Is GetType(Button) OrElse _
            CurrentControl.GetType() Is GetType(RadioButton) OrElse CurrentControl.GetType() Is GetType(CheckBox) _
            OrElse CurrentControl.GetType() Is GetType(CheckBoxList) Then

                DirectCast(CurrentControl, WebControl).Attributes.Add("onfocus", "try{document.getElementById('__LASTFOCUS').value=this.id}catch(e) {}")
            ElseIf CurrentControl.GetType() Is GetType(CheckBox) Then
                DirectCast(CurrentControl, CheckBox).InputAttributes.Add("onfocus", "try{document.getElementById('__LASTFOCUS').value=this.id}catch(e) {}")
            End If
            If (CurrentControl.HasControls()) Then
                For Each CurrentChildControl As Control In CurrentControl.Controls
                    HookOnFocus(CurrentChildControl)
                Next
            End If
        End Sub

        Public Shared Function CompararTotalizadores(origen_SubCanales As List(Of Comon.Clases.SubCanal), _
                                               origen_identificadorCliente As String, _
                                               origen_identificadorSubCliente As String, _
                                               origen_identificadorPtoServicio As String, _
                                               destino_SubCanales As List(Of Comon.Clases.SubCanal), _
                                               destino_identificadorCliente As String, _
                                               destino_identificadorSubCliente As String, _
                                               destino_identificadorPtoServicio As String) As Boolean

            Dim esIgual As Boolean = True

            If origen_SubCanales IsNot Nothing AndAlso destino_SubCanales IsNot Nothing _
                AndAlso origen_SubCanales.Count = destino_SubCanales.Count _
                AndAlso origen_identificadorCliente.Equals(destino_identificadorCliente) _
                AndAlso origen_identificadorSubCliente.Equals(destino_identificadorSubCliente) _
                AndAlso origen_identificadorPtoServicio.Equals(destino_identificadorPtoServicio) Then
                For Each _subcanal In origen_SubCanales
                    If destino_SubCanales.FirstOrDefault(Function(x) x.Identificador = _subcanal.Identificador) Is Nothing Then
                        esIgual = False
                    End If
                Next
            Else
                esIgual = False
            End If

            Return esIgual

        End Function

        Public Shared Function HayModificaciones(objetoOriginal As Object, objetoCopia As Object, _
                                             Optional ForzarDiferencias As Boolean = False) As Boolean

            If objetoOriginal Is Nothing AndAlso objetoCopia Is Nothing Then
                Return False
            ElseIf objetoCopia Is Nothing OrElse objetoOriginal Is Nothing Then
                Return True
            End If

            If ForzarDiferencias Then
                Return True
            End If

            If Serializar(objetoOriginal).Equals(Serializar(objetoCopia)) Then
                Return False
            End If

            Return True
        End Function

        Public Shared Function Serializar(Objeto As Object) As String

            Dim writer As StringWriter = New StringWriter
            Dim serializer As XmlSerializer = New XmlSerializer(Objeto.GetType())


            serializer.Serialize(writer, Objeto)


            Return writer.ToString()

        End Function

#End Region

    End Class

End Namespace