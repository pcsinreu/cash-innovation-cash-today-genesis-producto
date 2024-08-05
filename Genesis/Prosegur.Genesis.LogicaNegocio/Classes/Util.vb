Imports System.Data
Imports System.Configuration.ConfigurationManager
Imports System.IO
Imports System.Web.Caching
Imports Prosegur.Global
Imports Prosegur.Genesis.ContractoServicio
Imports System.Web
Imports Prosegur.Framework.Dicionario
Imports System.Runtime.Serialization.Formatters.Binary
Imports System.Xml.Serialization
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Parametros

Imports System.Configuration
Imports Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio
Imports Prosegur.Global.GesEfectivo
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon.Extenciones

Public Class Util


    Public Shared Function RecuperarMensagemTratada(ex As Exception) As String

        Dim MsgErro As String() = Nothing

        If ex.Message.Contains("ORA-20001") Then

            MsgErro = ex.Message.Split("#")

            If MsgErro.Count > 1 AndAlso Not String.IsNullOrEmpty(MsgErro(1)) Then
                Return MsgErro(1)
            End If

        ElseIf ex.Message.Contains("ORA-06508") Then
            MsgErro = ex.Message.Split(Chr(34))
            If MsgErro.Count > 1 AndAlso Not String.IsNullOrEmpty(MsgErro(1)) Then
                ' MSG provisoria
                Return "Error al intentar ejecutar la package: " & MsgErro(1) & "."
            End If
        End If

        If ex.InnerException IsNot Nothing Then
            Return RecuperarMensagemTratada(ex.InnerException)
        End If


        Return ex.ToString
    End Function


    ''' <summary>
    ''' Atribui o valor ao objeto passado, faz a conversão do tipo do banco para o tipo da propriedade.
    ''' </summary>
    ''' <param name="Valor"></param>
    ''' <param name="TipoCampo"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 18/11/2009 Criado
    ''' </history>
    Public Shared Function AtribuirValorObj(Valor As Object,
                                               TipoCampo As System.Type) As Object

        Dim Campo As New Object

        If TipoCampo Is GetType(Byte()) AndAlso Valor IsNot DBNull.Value Then
            Campo = Valor
        ElseIf Valor IsNot DBNull.Value AndAlso Not String.IsNullOrEmpty(Valor) Then
            If TipoCampo Is Nothing Then
                Campo = Valor
            ElseIf TipoCampo Is GetType(String) Then
                Campo = Convert.ToString(Valor)
            ElseIf TipoCampo Is GetType(Int16) Then
                Campo = Convert.ToInt16(Valor)
            ElseIf TipoCampo Is GetType(Int32) Then
                Campo = Convert.ToInt32(Valor)
            ElseIf TipoCampo Is GetType(Int64) Then
                Campo = Convert.ToInt64(Valor)
            ElseIf TipoCampo Is GetType(Int64?) Then
                Campo = Convert.ToInt64(Valor)
            ElseIf TipoCampo Is GetType(Decimal) Then
                Campo = Convert.ToDecimal(Valor)
            ElseIf TipoCampo Is GetType(Double) Then
                Campo = Convert.ToDouble(Valor)
            ElseIf TipoCampo Is GetType(Boolean) Then
                Campo = Convert.ToBoolean(Convert.ToInt16(Valor.ToString.Trim))
            ElseIf TipoCampo Is GetType(DateTime) Then
                Campo = Convert.ToDateTime(Valor)
            ElseIf TipoCampo Is GetType(Drawing.Color) Then
                Campo = Drawing.ColorTranslator.FromHtml("#" & Valor)
            End If
        Else

            If TipoCampo Is GetType(Decimal) Then
                Campo = 0
            ElseIf TipoCampo Is GetType(Int16) Then
                Campo = 0
            ElseIf TipoCampo Is GetType(Int32) Then
                Campo = 0
            ElseIf TipoCampo Is GetType(Int64) Then
                Campo = 0
            Else
                Campo = Nothing
            End If

        End If

        Return Campo
    End Function

    ''' <summary>
    ''' Retorna cópia do objeto passado como parâmetro
    ''' </summary>
    ''' <param name="obj"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ClonarObjeto(obj As Object) As Object

        If obj IsNot Nothing Then

            ' Create a memory stream and a formatter.
            Dim ms As New MemoryStream()
            Dim bf As New BinaryFormatter()

            ' Serialize the object into the stream.
            bf.Serialize(ms, obj)

            ' Position streem pointer back to first byte.
            ms.Seek(0, SeekOrigin.Begin)

            ' Deserialize into another object.
            ClonarObjeto = bf.Deserialize(ms)

            ' Release memory.
            ms.Close()

        Else

            ClonarObjeto = Nothing

        End If

    End Function
    Public Shared Sub TratarErroBugsnag(ex As Exception)

        If (Not (TypeOf ex Is Prosegur.Genesis.Excepcion.NegocioExcepcion)) Then

            Prosegur.BugsnagHelper.NotifyIfEnabled(ex)

        End If
    End Sub
    ''' <summary>
    ''' Validar erro tabela existente
    ''' </summary>
    ''' <param name="MensajeErrorGenerica"></param>
    ''' <param name="CodErro"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ValidarErroTabelaExiste(MensajeErrorGenerica As String, Optional CodErro As String = "") As String

        If Not String.IsNullOrEmpty(MensajeErrorGenerica) Then

            Dim palavras() As String = MensajeErrorGenerica.Split(" ")

            If (From p In palavras Where p.Trim.ToLower = Constantes.CONST_ERRO_BANCO4.ToLower).Count > 0 Then
                'Retorna erro de banco de dados
                Return Tradutor.Traduzir("gen_msg_error_tabela_existente")

            Else
                'Retorna erro de banco de dados
                Return MensajeErrorGenerica
            End If

        End If

        Return String.Empty

    End Function

    ''' <summary>
    ''' Gera arquivo de log na pasta do diretório virtual
    ''' </summary>
    ''' <param name="str"></param>
    ''' <remarks></remarks>
    Public Shared Sub GuardarLogExecucao(str As String)

        If AppSettings("GravarLogExecucao") IsNot Nothing _
            AndAlso AppSettings("GravarLogExecucao") = "1" Then
            Dim logFile As New StreamWriter(System.Web.HttpContext.Current.Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath) & "/log.txt", True)
            logFile.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") & " - " & str)
            logFile.Close()
        End If

    End Sub

    Private Shared Sub cachingDelegaciones()
        Dim Peticion As New Login.ObtenerDelegaciones.Peticion
        Dim Respuesta As Seguridad.ContractoServicio.ObtenerDelegaciones.Respuesta
        Respuesta = AccionObtenerDelegaciones.ObtenerDelegaciones(Peticion)

        Dim c As Cache = HttpRuntime.Cache
        c.Add("Delegaciones", Respuesta.Delegaciones, Nothing, DateTime.Now.AddHours(24), Cache.NoSlidingExpiration, CacheItemPriority.High, Nothing)
    End Sub

    Public Shared Function cachingGetDelegacion(codDeleacion As String) As Seguridad.ContractoServicio.ObtenerDelegaciones.Delegacion
        Dim c As Cache = HttpRuntime.Cache
        If (IsNothing(c("Delegaciones"))) Then
            cachingDelegaciones()
        End If
        Dim delegaciones As Seguridad.ContractoServicio.ObtenerDelegaciones.DelegacionColeccion = CType(c("Delegaciones"), Seguridad.ContractoServicio.ObtenerDelegaciones.DelegacionColeccion)

        Return If(delegaciones IsNot Nothing, delegaciones.FirstOrDefault(Function(f) f.Codigo = codDeleacion), Nothing)

    End Function

    Public Shared Function GetDateTime(codDeleacion As String) As DateTime
        Dim deleg = cachingGetDelegacion(codDeleacion)
        Dim dt As DateTime
        dt = If(deleg IsNot Nothing, DateTime.UtcNow.AddMinutes(deleg.GMT), DateTime.Now)
        If deleg IsNot Nothing AndAlso deleg.VeranoAjuste > 0 AndAlso
            (dt.Ticks > deleg.VeranoFechaHoraIni.Ticks AndAlso dt.Ticks < deleg.VeranoFechaHoraFin.Ticks) AndAlso
            (deleg.VeranoFechaHoraIni.Ticks <> deleg.VeranoFechaHoraFin.Ticks) Then
            dt = dt.AddMinutes(deleg.VeranoAjuste)
        End If
        Return dt

    End Function


    Public Shared Function GetDateTime(deleg As Clases.Delegacion) As DateTime

        Dim dt As DateTime
        dt = If(deleg IsNot Nothing, DateTime.UtcNow.AddMinutes(deleg.HusoHorarioEnMinutos), DateTime.Now)
        If deleg IsNot Nothing AndAlso deleg.AjusteHorarioVerano > 0 AndAlso
            (dt.Ticks > deleg.FechaHoraVeranoInicio.Ticks AndAlso dt.Ticks < deleg.FechaHoraVeranoFin.Ticks) AndAlso
            (deleg.FechaHoraVeranoInicio.Ticks <> deleg.FechaHoraVeranoFin.Ticks) Then
            dt = dt.AddMinutes(deleg.AjusteHorarioVerano)
        End If
        Return dt

    End Function

    Public Shared Function GetGMTVeranoAjuste(codDeleacion As String) As Short
        Dim deleg = cachingGetDelegacion(codDeleacion)
        Dim dt As DateTime
        dt = If(deleg IsNot Nothing, DateTime.UtcNow.AddMinutes(deleg.GMT), DateTime.Now)
        If deleg IsNot Nothing AndAlso deleg.VeranoAjuste > 0 AndAlso
            (dt.Ticks > deleg.VeranoFechaHoraIni.Ticks AndAlso dt.Ticks < deleg.VeranoFechaHoraFin.Ticks) AndAlso
            (deleg.VeranoFechaHoraIni.Ticks <> deleg.VeranoFechaHoraFin.Ticks) Then
            Return deleg.GMT + deleg.VeranoAjuste
        End If
        Return If(deleg IsNot Nothing, deleg.GMT, 0)

    End Function

    ''' <summary>
    ''' Verifica se o campo foi preenchido
    ''' </summary>
    ''' <param name="campo"></param>
    ''' <param name="msgErro"></param>
    ''' <param name="TipoCampo"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [gustavo.seabra] 03/01/2013 - Criado
    ''' </history>
    Public Shared Sub ValidarCampoObrigatorio(campo As Object, msgErro As String,
                                              TipoCampo As System.Type, bolColeccion As Boolean,
                                              bolMsgAtributo As Boolean)

        msgErro = Tradutor.Traduzir(msgErro)
        If msgErro.StartsWith("[") AndAlso msgErro.EndsWith("]") Then
            'Retira o primeiro é último caracter
            msgErro = msgErro.Substring(1, msgErro.Length - 2)
        End If

        If TipoCampo Is GetType(String) Then

            If String.IsNullOrEmpty(campo) Then

                If bolMsgAtributo Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT,
                                 String.Format(Tradutor.Traduzir("Gen_msg_atributo"),
                                               msgErro))
                Else
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Tradutor.Traduzir(msgErro))
                End If

            End If

        ElseIf TipoCampo Is GetType(DateTime) Then

            If campo.Equals(Date.MinValue) Then

                If bolMsgAtributo Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT,
                                 String.Format(Tradutor.Traduzir("Gen_msg_atributo"),
                                               msgErro))
                Else
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Tradutor.Traduzir(msgErro))
                End If

            End If

        ElseIf TipoCampo Is GetType(Nullable(Of Decimal)) Then

            If campo Is Nothing Then

                If bolMsgAtributo Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT,
                                 String.Format(Tradutor.Traduzir("Gen_msg_atributo"),
                                               msgErro))
                Else
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Tradutor.Traduzir(msgErro))
                End If

            End If

        ElseIf TipoCampo Is GetType(Nullable(Of Boolean)) Then

            If campo Is Nothing Then

                If bolMsgAtributo Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT,
                                 String.Format(Tradutor.Traduzir("Gen_msg_atributo"),
                                               msgErro))
                Else
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Tradutor.Traduzir(msgErro))
                End If

            End If

        ElseIf TipoCampo Is GetType(Nullable(Of Integer)) Then

            If campo Is Nothing Then

                If bolMsgAtributo Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT,
                                 String.Format(Tradutor.Traduzir("Gen_msg_atributo"),
                                               msgErro))
                Else
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Tradutor.Traduzir(msgErro))
                End If

            End If

        Else

            If bolColeccion Then

                If campo Is Nothing OrElse campo.count = 0 Then

                    If bolMsgAtributo Then
                        Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT,
                                     String.Format(Tradutor.Traduzir("Gen_msg_atributo"),
                                                   msgErro))
                    Else
                        Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Tradutor.Traduzir(msgErro))
                    End If
                End If

            Else

                If campo Is Nothing Then

                    If bolMsgAtributo Then
                        Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT,
                                     String.Format(Tradutor.Traduzir("Gen_msg_atributo"),
                                                   msgErro))
                    Else
                        Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Tradutor.Traduzir(msgErro))
                    End If

                End If

            End If

        End If
    End Sub

    ''' <summary>
    ''' Verifica se o registro existe na tabela.
    ''' </summary>
    ''' <param name="tabela"></param>
    ''' <param name="coluna"></param>
    ''' <param name="valor"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function RegistroExiste(tabela As Enumeradores.Tabela, coluna As String, valor As String) As Boolean

        Try
            Return AccesoDatos.Util.RegistroExiste(tabela, coluna, valor)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    ''' <summary>
    ''' Recupera los valores de los parametros por CodigoAplicacion y CodigoParametro
    ''' </summary>
    ''' <param name="CodigoAplicacion"></param>
    ''' <param name="CodigoParametro"></param>
    ''' <returns></returns>
    Public Shared Function GetParametros(CodigoAplicacion As String, CodigoParametro As String) As List(Of Parametro)

        Dim listaParametro As New List(Of Parametro)
        listaParametro = AccesoDatos.Genesis.Parametros.GetParametros(CodigoAplicacion, CodigoParametro)

        Return listaParametro
    End Function

    ''' <summary>
    ''' Recupera parâmetros do IAC no nivel da delegação 
    ''' </summary>
    ''' <param name="CodigoAplicacion"></param>
    ''' <param name="CodigoDelegacion"></param>
    ''' <param name="Parametros"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ParametrosDelegacionPais(CodigoAplicacion As String, CodigoDelegacion As String, Parametros As GetParametrosDelegacionPais.ParametroColeccion) As GetParametrosDelegacionPais.ParametroRespuestaColeccion
        Dim peticionParametrosDelegacionPais As New GetParametrosDelegacionPais.Peticion()
        peticionParametrosDelegacionPais.CodigoAplicacion = CodigoAplicacion
        peticionParametrosDelegacionPais.CodigoDelegacion = CodigoDelegacion
        peticionParametrosDelegacionPais.Parametros = Parametros
        Dim objNegocioIAC As New IAC.LogicaNegocio.AccionIntegracion()
        Dim respuestaParametrosDelegacionPais As IAC.Integracion.ContractoServicio.GetParametrosDelegacionPais.Respuesta = objNegocioIAC.GetParametrosDelegacionPais(peticionParametrosDelegacionPais)
        If respuestaParametrosDelegacionPais.CodigoError > Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT Then
            Throw New Excepcion.NegocioExcepcion(respuestaParametrosDelegacionPais.CodigoError, respuestaParametrosDelegacionPais.MensajeError)
        End If
        Return respuestaParametrosDelegacionPais.Parametros
    End Function

    Public Shared Sub VerificaInformacionesToken(peticion As BasePeticion)

        ' valida petição
        If peticion Is Nothing Then
            Throw New Excepcion.NegocioExcepcion(Tradutor.Traduzir("NUEVO_SALDOS_TOKEN_PETICION_OBLIGATORIA"))
        Else

            ' verifica se existe configuração de token para ser validado
            If AppSettings("Token") IsNot Nothing AndAlso Not String.IsNullOrEmpty(AppSettings("Token")) Then

                'valida se foi informado o token
                If String.IsNullOrEmpty(peticion.Token) OrElse Not AppSettings("Token").Equals(peticion.Token) Then
                    Throw New Excepcion.NegocioExcepcion(String.Format(Tradutor.Traduzir("NUEVO_SALDOS_TOKEN_TOKEN_INVALIDO"), peticion.Token))
                End If
            End If

        End If
    End Sub

    Public Shared Sub VerificaInformacionesToken(peticion As Contractos.Integracion.Comon.BasePeticion)

        ' valida petição
        If peticion Is Nothing Then
            Throw New Excepcion.NegocioExcepcion(Tradutor.Traduzir("NUEVO_SALDOS_TOKEN_PETICION_OBLIGATORIA"))
        Else

            ' verifica se existe configuração de token para ser validado
            If AppSettings("Token") IsNot Nothing AndAlso Not String.IsNullOrEmpty(AppSettings("Token")) Then

                'valida se foi informado o token
                If String.IsNullOrEmpty(peticion.tokenAcceso) OrElse Not AppSettings("Token").Equals(peticion.tokenAcceso) Then
                    Throw New Excepcion.NegocioExcepcion(String.Format(Tradutor.Traduzir("NUEVO_SALDOS_TOKEN_TOKEN_INVALIDO"), peticion.tokenAcceso))
                End If
            End If

        End If
    End Sub

    Public Shared Sub BorrarItemsDivisasValoresCantidades(ByRef divisas As ObservableCollection(Of Clases.Divisa), borrarValoresPositivos As Boolean)

        ' para não ter que gerar cópia de objetos em memória e garantir a integridade deles
        ' a remoção dos valores que se enquadram nas validações é feita na própria coleção (por referência)
        ' por este motivo, para que não haja um erro de alteração na coleção ao se remover um item
        ' as coleções abaixo são lidas utilizando um "for" inverso, ou seja, lendo do maior índice
        ' para o menor, neste caso, mesmo removendo itens da coleção, o índice sempre esta de acordo
        ' com a quantidade final

        If divisas IsNot Nothing AndAlso divisas.Count() > 0 Then

            Dim totalDivisas As Integer = divisas.Count() - 1
            For div As Integer = totalDivisas To 0 Step -1

                If divisas(div) Is Nothing Then
                    ' remove a divisa caso seja vazia
                    divisas.RemoveAt(div)
                Else

                    ' denominações
                    If divisas(div).Denominaciones IsNot Nothing AndAlso divisas(div).Denominaciones.Count() > 0 Then
                        Dim totalDenominaciones As Integer = divisas(div).Denominaciones.Count() - 1
                        For den As Integer = totalDenominaciones To 0 Step -1
                            ' remove valores positivos
                            If borrarValoresPositivos AndAlso (divisas(div).Denominaciones(den) Is Nothing OrElse divisas(div).Denominaciones(den).ValorDenominacion Is Nothing OrElse divisas(div).Denominaciones(den).ValorDenominacion.Count() = 0 OrElse Not divisas(div).Denominaciones(den).ValorDenominacion.ToList().Exists(Function(v) v.Importe <= 0 OrElse v.Cantidad <= 0)) Then
                                ' remove a denominação caso seja vazia ou não tenha valores
                                divisas(div).Denominaciones.RemoveAt(den)
                                ' remove valores negativos
                            ElseIf Not borrarValoresPositivos AndAlso (divisas(div).Denominaciones(den) Is Nothing OrElse divisas(div).Denominaciones(den).ValorDenominacion Is Nothing OrElse divisas(div).Denominaciones(den).ValorDenominacion.Count() = 0 OrElse Not divisas(div).Denominaciones(den).ValorDenominacion.ToList().Exists(Function(v) v.Importe >= 0 OrElse v.Cantidad >= 0)) Then
                                ' remove a denominação caso seja vazia ou não tenha valores
                                divisas(div).Denominaciones.RemoveAt(den)
                            Else
                                If divisas(div).Denominaciones(den).ValorDenominacion IsNot Nothing AndAlso divisas(div).Denominaciones(den).ValorDenominacion.Count() > 0 Then
                                    Dim totalValoresDenominaciones As Integer = divisas(div).Denominaciones(den).ValorDenominacion.Count() - 1
                                    For valden As Integer = totalValoresDenominaciones To 0 Step -1
                                        If divisas(div).Denominaciones(den).ValorDenominacion(valden) Is Nothing OrElse (borrarValoresPositivos AndAlso (divisas(div).Denominaciones(den).ValorDenominacion(valden).Importe >= 0 AndAlso divisas(div).Denominaciones(den).ValorDenominacion(valden).Cantidad >= 0)) OrElse (Not borrarValoresPositivos AndAlso (divisas(div).Denominaciones(den).ValorDenominacion(valden).Importe <= 0 AndAlso divisas(div).Denominaciones(den).ValorDenominacion(valden).Cantidad <= 0)) Then
                                            ' remove o valor relacionado a denominação
                                            divisas(div).Denominaciones(den).ValorDenominacion.RemoveAt(valden)
                                        End If
                                    Next
                                    If divisas(div).Denominaciones(den).ValorDenominacion.Count() = 0 Then
                                        ' remove a denominação (caso não tenha mais valores)
                                        divisas(div).Denominaciones.RemoveAt(den)
                                    End If
                                End If
                            End If
                        Next
                    End If

                    ' medio de pagos
                    If divisas(div).MediosPago IsNot Nothing AndAlso divisas(div).MediosPago.Count() > 0 Then
                        Dim totalMediosPago As Integer = divisas(div).MediosPago.Count() - 1
                        For med As Integer = totalMediosPago To 0 Step -1
                            If borrarValoresPositivos AndAlso (divisas(div).MediosPago(med) Is Nothing OrElse divisas(div).MediosPago(med).Valores Is Nothing OrElse divisas(div).MediosPago(med).Valores.Count() = 0 OrElse Not divisas(div).MediosPago(med).Valores.ToList().Exists(Function(v) Not v.Importe <= 0 OrElse Not v.Cantidad <= 0)) Then
                                ' remove o meio de pagamento caso seja vazio ou não tenha valores
                                divisas(div).MediosPago.RemoveAt(med)
                            ElseIf Not borrarValoresPositivos AndAlso (divisas(div).MediosPago(med) Is Nothing OrElse divisas(div).MediosPago(med).Valores Is Nothing OrElse divisas(div).MediosPago(med).Valores.Count() = 0 OrElse Not divisas(div).MediosPago(med).Valores.ToList().Exists(Function(v) Not v.Importe >= 0 OrElse Not v.Cantidad >= 0)) Then
                                ' remove o meio de pagamento caso seja vazio ou não tenha valores
                                divisas(div).MediosPago.RemoveAt(med)
                            Else
                                If divisas(div).MediosPago(med).Valores IsNot Nothing AndAlso divisas(div).MediosPago(med).Valores.Count() > 0 Then
                                    Dim totalValoresMediosPago As Integer = divisas(div).MediosPago(med).Valores.Count() - 1
                                    For valmed As Integer = totalValoresMediosPago To 0 Step -1
                                        If divisas(div).MediosPago(med).Valores(valmed) Is Nothing OrElse (borrarValoresPositivos AndAlso (divisas(div).MediosPago(med).Valores(valmed).Importe >= 0 AndAlso divisas(div).MediosPago(med).Valores(valmed).Cantidad >= 0)) OrElse (Not borrarValoresPositivos AndAlso (divisas(div).MediosPago(med).Valores(valmed).Importe <= 0 AndAlso divisas(div).MediosPago(med).Valores(valmed).Cantidad <= 0)) Then
                                            ' remove o valor relacionado ao meio de pagamento
                                            divisas(div).MediosPago(med).Valores.RemoveAt(valmed)
                                        End If
                                    Next
                                    If divisas(div).MediosPago(med).Valores.Count() = 0 Then
                                        ' remove o meio de pagamento (caso não tenha mais valores)
                                        divisas(div).MediosPago.RemoveAt(med)
                                    End If
                                End If
                            End If
                        Next
                    End If

                    ' geral
                    If divisas(div).ValoresTotalesDivisa IsNot Nothing AndAlso divisas(div).ValoresTotalesDivisa.Count() > 0 AndAlso ((borrarValoresPositivos AndAlso divisas(div).ValoresTotalesDivisa.ToList().Exists(Function(v) v.Importe >= 0)) OrElse (Not borrarValoresPositivos AndAlso divisas(div).ValoresTotalesDivisa.ToList().Exists(Function(v) v.Importe <= 0))) Then
                        Dim totalItems As Integer = divisas(div).ValoresTotalesDivisa.Count() - 1
                        For ger As Integer = totalItems To 0 Step -1
                            If (borrarValoresPositivos AndAlso divisas(div).ValoresTotalesDivisa(ger).Importe >= 0) OrElse (Not borrarValoresPositivos AndAlso divisas(div).ValoresTotalesDivisa(ger).Importe <= 0) Then
                                ' remove o valor geral
                                divisas(div).ValoresTotalesDivisa.RemoveAt(ger)
                            End If
                        Next
                    End If

                    ' total efectivo
                    If divisas(div).ValoresTotalesEfectivo IsNot Nothing AndAlso divisas(div).ValoresTotalesEfectivo.Count() > 0 AndAlso ((borrarValoresPositivos AndAlso divisas(div).ValoresTotalesEfectivo.ToList().Exists(Function(v) v.Importe >= 0)) OrElse (Not borrarValoresPositivos AndAlso divisas(div).ValoresTotalesEfectivo.ToList().Exists(Function(v) v.Importe <= 0))) Then
                        Dim totalItems As Integer = divisas(div).ValoresTotalesEfectivo.Count() - 1
                        For totefe As Integer = totalItems To 0 Step -1
                            If (borrarValoresPositivos AndAlso divisas(div).ValoresTotalesEfectivo(totefe).Importe >= 0) OrElse (Not borrarValoresPositivos AndAlso divisas(div).ValoresTotalesEfectivo(totefe).Importe <= 0) Then
                                ' remove o valor total efectivo
                                divisas(div).ValoresTotalesEfectivo.RemoveAt(totefe)
                            End If
                        Next
                    End If

                    ' total tipo medio de pago
                    If divisas(div).ValoresTotalesTipoMedioPago IsNot Nothing AndAlso divisas(div).ValoresTotalesTipoMedioPago.Count() > 0 AndAlso ((borrarValoresPositivos AndAlso divisas(div).ValoresTotalesTipoMedioPago.ToList().Exists(Function(v) v.Importe >= 0 AndAlso v.Cantidad >= 0)) OrElse (Not borrarValoresPositivos AndAlso divisas(div).ValoresTotalesTipoMedioPago.ToList().Exists(Function(v) v.Importe <= 0 AndAlso v.Cantidad <= 0))) Then
                        Dim totalItems As Integer = divisas(div).ValoresTotalesTipoMedioPago.Count() - 1
                        For totmed As Integer = totalItems To 0 Step -1
                            If (borrarValoresPositivos AndAlso divisas(div).ValoresTotalesTipoMedioPago(totmed).Importe >= 0 AndAlso divisas(div).ValoresTotalesTipoMedioPago(totmed).Cantidad >= 0) OrElse (Not borrarValoresPositivos AndAlso divisas(div).ValoresTotalesTipoMedioPago(totmed).Importe <= 0 AndAlso divisas(div).ValoresTotalesTipoMedioPago(totmed).Cantidad <= 0) Then
                                ' remove o valor total medio de pago
                                divisas(div).ValoresTotalesTipoMedioPago.RemoveAt(totmed)
                            End If
                        Next
                    End If

                    ' se após todas as exclusões individuais não restar mais nenhuma propriedade com valores
                    ' deverá excluir a divisa
                    If (divisas(div).Denominaciones Is Nothing OrElse divisas(div).Denominaciones.Count() = 0) AndAlso
                        (divisas(div).MediosPago Is Nothing OrElse divisas(div).MediosPago.Count() = 0) AndAlso
                        (divisas(div).ValoresTotalesDivisa Is Nothing OrElse divisas(div).ValoresTotalesDivisa.Count() = 0) AndAlso
                        (divisas(div).ValoresTotalesEfectivo Is Nothing OrElse divisas(div).ValoresTotalesEfectivo.Count() = 0) AndAlso
                        (divisas(div).ValoresTotalesTipoMedioPago Is Nothing OrElse divisas(div).ValoresTotalesTipoMedioPago.Count() = 0) Then
                        divisas.RemoveAt(div)
                    End If

                End If

            Next

        End If

    End Sub

    Public Shared Sub ValidarSaldoPuestoMedioPago(ByRef remesa As Clases.Remesa, codigoPlanta As String, codigoDelegacion As String, trabajaPorPuesto As Boolean, ByRef mensajeValidacion As System.Text.StringBuilder)

        If remesa IsNot Nothing AndAlso remesa.EsMedioPago Then

            Dim codigosSectores As ObservableCollection(Of String) = Nothing
            Dim codigoSectorPadre As String = String.Empty

            If trabajaPorPuesto Then
                codigosSectores = New ObservableCollection(Of String) From {remesa.Cuenta.Sector.Codigo}
            Else
                codigoSectorPadre = remesa.Cuenta.Sector.Codigo
            End If

            Dim codigoCliente As String = remesa.Cuenta.Cliente.Codigo
            Dim codigoSubCliente As String = remesa.Cuenta.SubCliente.Codigo
            Dim codigoPuntoServicio As String = remesa.Cuenta.PuntoServicio.Codigo
            Dim codigoSubCanal As String = remesa.Cuenta.SubCanal.Codigo


            Util.ValidarCampoObrigatorio(codigoCliente, "CodigoCliente", GetType(String), False, True)

            Dim clienteTotalizador = AccesoDatos.Genesis.Cliente.RecuperarClienteTotalizadorSaldo(codigoCliente, codigoSubCliente, codigoPuntoServicio, codigoSubCanal)
            Dim saldos As ObservableCollection(Of Clases.Saldo)

            If clienteTotalizador Is Nothing Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Tradutor.Traduzir("NUEVO_SALDOS_029_no_encontro_cliente_saldos"), codigoCliente))
            Else

                codigoSubCliente = String.Empty
                codigoPuntoServicio = String.Empty

                If clienteTotalizador.SubClientes IsNot Nothing AndAlso clienteTotalizador.SubClientes.Count > 0 Then
                    codigoSubCliente = clienteTotalizador.SubClientes.First.Codigo

                    If clienteTotalizador.SubClientes.First.PuntosServicio IsNot Nothing AndAlso clienteTotalizador.SubClientes.First.PuntosServicio.Count > 0 Then
                        codigoPuntoServicio = clienteTotalizador.SubClientes.First.PuntosServicio.First.Codigo
                    End If

                End If


                saldos = AccesoDatos.GenesisSaldos.SaldoEfectivo.RecuperarSaldoPorCuenta(codigosSectores, clienteTotalizador.Codigo,
                                                                                         codigoSubCliente,
                                                                                         codigoPuntoServicio,
                                                                                         codigoSectorPadre,
                                                                                         codigoPlanta,
                                                                                         codigoDelegacion,
                                                                                         codigoSubCanal, True, True)

            End If

            Dim divisasPuesto As New ObservableCollection(Of Clases.Divisa)
            Dim valoresTotalesTipoMedioPagoPuesto As New ObservableCollection(Of Clases.ValorTipoMedioPago)

            If saldos IsNot Nothing AndAlso saldos.Count > 0 Then
                For Each saldo In saldos.Where(Function(s) s.Divisas IsNot Nothing AndAlso s.Divisas.Count > 0)
                    divisasPuesto.AddRange(saldo.Divisas)
                Next
            End If

            Comon.Util.UnificaItemsDivisas(divisasPuesto)
            Comon.Util.BorrarItemsDivisasSinValoresCantidades(divisasPuesto)

            ' não existe saldo no posto
            If divisasPuesto Is Nothing OrElse divisasPuesto.Count = 0 Then
                mensajeValidacion.AppendLine(Tradutor.Traduzir("NUEVO_SALIDAS_CERRARREMESA_SALDODISTINTO"))
                Exit Sub
            End If

            Dim _ListaMediosPago As New ObservableCollection(Of Clases.MedioPago)
            Dim ListaAgrupada As New ObservableCollection(Of List(Of Clases.MedioPago))
            For Each divisa In divisasPuesto.Where(Function(d) d.MediosPago IsNot Nothing AndAlso d.MediosPago.Count > 0)

                _ListaMediosPago.AddRange(divisa.MediosPago.OrderBy(Function(f) f.Descripcion))
                _ListaMediosPago.GroupBy(Function(f) f.Tipo).ToList.ForEach(Sub(s) ListaAgrupada.Add(New List(Of Clases.MedioPago)(s.ToList())))

                AgruparTiposMediosPago(ListaAgrupada, valoresTotalesTipoMedioPagoPuesto)

                ValidarValores(divisa.Identificador, remesa, valoresTotalesTipoMedioPagoPuesto, mensajeValidacion)

                If mensajeValidacion.Length > 0 Then
                    Exit Sub
                End If

            Next divisa

            'Se não possui saldo no posto
            If ListaAgrupada.Count = 0 Then
                mensajeValidacion.AppendLine(Tradutor.Traduzir("NUEVO_SALIDAS_CERRARREMESA_SALDODISTINTO"))
                Exit Sub
            End If

            '•	Rellenar el desglose del documento: Para generar el documento donde se va crear la remesa de salida en Nuevo Saldos el Sistema deberá tomar el saldo detallado del Puesto y agregar como detalle de la remesa a ser creada en Nuevo Saldos. De esta forma el Sistema irá impactar directamente en el saldo del puesto. 
            divisasPuesto.Foreach(Sub(s)
                                      If s.MediosPago IsNot Nothing AndAlso s.MediosPago.Count > 0 Then

                                          s.MediosPago.Foreach(Sub(mp)
                                                                   If mp.Valores IsNot Nothing AndAlso mp.Valores.Count > 0 Then
                                                                       mp.Valores.Foreach(Sub(v)
                                                                                              v.TipoValor = Enumeradores.TipoValor.Declarado
                                                                                          End Sub)
                                                                   End If
                                                               End Sub)
                                      End If
                                  End Sub)

            For Each div In remesa.Divisas

                If div IsNot Nothing AndAlso div.ValoresTotalesTipoMedioPago IsNot Nothing _
                    AndAlso div.ValoresTotalesTipoMedioPago.Count > 0 Then

                    Dim divisaPuesto = divisasPuesto.Where(Function(d) d IsNot Nothing AndAlso d.Identificador = div.Identificador).FirstOrDefault
                    'Se o saldo do posto tem saldo para a divisa da remessa
                    If divisaPuesto IsNot Nothing AndAlso divisaPuesto.MediosPago IsNot Nothing AndAlso divisaPuesto.MediosPago.Count > 0 Then

                        For Each Valor In div.ValoresTotalesTipoMedioPago

                            If Valor IsNot Nothing Then
                                divisaPuesto.MediosPago.Foreach(Sub(m)
                                                                    If m IsNot Nothing AndAlso m.Valores IsNot Nothing AndAlso m.Valores.Count > 0 _
                                                                       AndAlso m.Tipo = Valor.TipoMedioPago Then

                                                                        If div.MediosPago Is Nothing Then div.MediosPago = New ObservableCollection(Of Clases.MedioPago)
                                                                        div.MediosPago.Add(m)

                                                                    End If
                                                                End Sub)
                            End If

                        Next

                    End If

                End If

            Next

            'remesa.Divisas = divisasPuesto

            '•	Bultos cuadrados: El Sistema deberá gravar los bultos cómo cuadrados directamente, o sea el flujo de cuadre no llevará en consideración estas remesas con medio pago.
            If remesa.Bultos IsNot Nothing AndAlso remesa.Bultos.Count > 0 Then
                remesa.Bultos.Foreach(Sub(b)
                                          b.Cuadrado = True
                                      End Sub)
            End If

        End If

    End Sub

    Private Shared Sub AgruparTiposMediosPago(ByRef ListaAgrupada As ObservableCollection(Of List(Of Clases.MedioPago)),
                                                   ByRef valoresTotalesTipoMedioPagoPuesto As ObservableCollection(Of Clases.ValorTipoMedioPago))

        If ListaAgrupada IsNot Nothing AndAlso ListaAgrupada.Count > 0 Then
            For Each tiposMP In ListaAgrupada
                For Each mp In tiposMP
                    If mp.Valores IsNot Nothing AndAlso mp.Valores.Count > 0 Then
                        If Not valoresTotalesTipoMedioPagoPuesto.Exists(Function(e) e.TipoMedioPago = mp.Tipo) Then
                            Dim vltipomediopago As New Clases.ValorTipoMedioPago With
                                {.Cantidad = mp.Valores.Sum(Function(s) s.Cantidad),
                                 .Importe = mp.Valores.Sum(Function(s) s.Importe),
                                 .TipoMedioPago = mp.Tipo
                                }
                            valoresTotalesTipoMedioPagoPuesto.Add(vltipomediopago)
                        Else
                            Dim vltipomediopago = valoresTotalesTipoMedioPagoPuesto.Where(Function(vl) vl.TipoMedioPago = mp.Tipo).FirstOrDefault
                            If vltipomediopago IsNot Nothing Then
                                vltipomediopago.Cantidad += mp.Valores.Sum(Function(s) s.Cantidad)
                                vltipomediopago.Importe += mp.Valores.Sum(Function(s) s.Importe)
                            End If
                        End If
                    End If
                Next mp
            Next tiposMP
        End If

    End Sub

    Private Shared Sub ValidarValores(identificadorDivisa As String,
                                      remesa As Clases.Remesa,
                                      ByRef valoresTotalesTipoMedioPagoPuesto As ObservableCollection(Of Clases.ValorTipoMedioPago),
                                      ByRef mensajevalidacion As System.Text.StringBuilder)

        Dim divRem = remesa.Divisas.Where(Function(d) d.Identificador = identificadorDivisa).FirstOrDefault

        If divRem Is Nothing OrElse divRem.ValoresTotalesTipoMedioPago Is Nothing OrElse divRem.ValoresTotalesTipoMedioPago.Count = 0 Then
            mensajevalidacion.AppendLine(Tradutor.Traduzir("NUEVO_SALIDAS_CERRARREMESA_SALDODISTINTO"))
            Exit Sub
        End If

        For Each valorRemesa In divRem.ValoresTotalesTipoMedioPago

            If valoresTotalesTipoMedioPagoPuesto.Exists(Function(tmp) tmp.TipoMedioPago = valorRemesa.TipoMedioPago) Then

                If valoresTotalesTipoMedioPagoPuesto.Where(Function(tmp) tmp.TipoMedioPago = valorRemesa.TipoMedioPago).Sum(Function(s) s.Importe) <> valorRemesa.Importe Then
                    mensajevalidacion.AppendLine(Tradutor.Traduzir("NUEVO_SALIDAS_CERRARREMESA_SALDODISTINTO"))
                    Exit Sub
                End If

            Else
                mensajevalidacion.AppendLine(Tradutor.Traduzir("NUEVO_SALIDAS_CERRARREMESA_SALDODISTINTO"))
                Exit Sub
            End If

        Next

    End Sub

    Public Shared Function RecuperarParametroIACString(codigoAplicacion As String, codigoDelegacion As String, codigoParametro As String) As String

        'recupera o parâmetro CrearConfiguiracionNivelSaldo do IAC
        Dim parametro As IAC.Integracion.ContractoServicio.GetParametrosDelegacionPais.ParametroRespuestaColeccion
        parametro = ParametrosDelegacionPais(codigoAplicacion, codigoDelegacion,
                                                   New IAC.Integracion.ContractoServicio.GetParametrosDelegacionPais.ParametroColeccion() From
                                                            {
                                                                New IAC.Integracion.ContractoServicio.GetParametrosDelegacionPais.Parametro() With {.CodigoParametro = codigoParametro}
                                                            })


        If parametro IsNot Nothing AndAlso parametro.Count > 0 AndAlso Not String.IsNullOrEmpty(parametro.FirstOrDefault.ValorParametro) Then
            Return parametro.FirstOrDefault.ValorParametro
        End If

        Return String.Empty

    End Function

    Public Shared Sub RecuperarDireccionesLegado(direcciones As List(Of Comon.Clases.Legado.EnumDireccionSerivicioLegado),
                                                 codigoDelegacion As String,
                                                 codigoAplicacion As String)

        If direcciones IsNot Nothing AndAlso direcciones.Count > 0 Then

            Dim _direccion As String = Nothing

            For Each _dir In direcciones

                Select Case _dir

                    Case Clases.Legado.EnumDireccionSerivicioLegado.Documento

                        Comon.Clases.Legado.DireccionServicioLegado.Documento_Service = Util.RecuperarParametroIACString(codigoAplicacion, codigoDelegacion, Parametros_v2.Codigos.RecepcionyEnvio.Delegacion.DireccionDocumentos)

                    Case Clases.Legado.EnumDireccionSerivicioLegado.Ruta

                        Comon.Clases.Legado.DireccionServicioLegado.Ruta_Service = Util.RecuperarParametroIACString(Comon.Constantes.CODIGO_APLICACION_GENESIS_RECEPCION_Y_ENVIO, codigoDelegacion, Parametros_v2.Codigos.RecepcionyEnvio.Delegacion.DireccionRutas)

                    Case Clases.Legado.EnumDireccionSerivicioLegado.ProgramacionServicio

                        Comon.Clases.Legado.DireccionServicioLegado.ProgramacionServicio_Service = Util.RecuperarParametroIACString(Comon.Constantes.CODIGO_APLICACION_GENESIS_RECEPCION_Y_ENVIO, codigoDelegacion, Parametros_v2.Codigos.RecepcionyEnvio.Delegacion.DireccionProgramacionServicios)

                    Case Clases.Legado.EnumDireccionSerivicioLegado.Ot

                        Comon.Clases.Legado.DireccionServicioLegado.Ot_Service = Util.RecuperarParametroIACString(Comon.Constantes.CODIGO_APLICACION_GENESIS_RECEPCION_Y_ENVIO, codigoDelegacion, Parametros_v2.Codigos.RecepcionyEnvio.Delegacion.DireccionOTs)

                    Case Clases.Legado.EnumDireccionSerivicioLegado.Modulo

                        Comon.Clases.Legado.DireccionServicioLegado.Modulo_Service = Util.RecuperarParametroIACString(Comon.Constantes.CODIGO_APLICACION_GENESIS_RECEPCION_Y_ENVIO, codigoDelegacion, Parametros_v2.Codigos.RecepcionyEnvio.Delegacion.DireccionModulos)

                    Case Clases.Legado.EnumDireccionSerivicioLegado.Servicio

                        Comon.Clases.Legado.DireccionServicioLegado.Servicio_Service = Util.RecuperarParametroIACString(Comon.Constantes.CODIGO_APLICACION_GENESIS_RECEPCION_Y_ENVIO, codigoDelegacion, Parametros_v2.Codigos.RecepcionyEnvio.Delegacion.DireccionServicios)

                End Select

            Next

        End If

    End Sub

    ''' <summary>
    ''' Recebi una data y retorna la data que debe ser grabada en la BBDD
    ''' </summary>
    ''' <param name="data"></param>
    ''' <param name="delegacion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function DataHoraGMT_GrabarEnLaBase(data As DateTime, ByRef delegacion As Clases.Delegacion) As DateTime
        Return AccesoDatos.Util.DataHoraGMT_GrabarEnLaBase(data, delegacion)
    End Function

    Public Shared Function DataHoraGMT_RecuperardeLaBase(data As DateTime, ByRef delegacion As Clases.Delegacion) As DateTime
        Return AccesoDatos.Util.DataHoraGMT_RecuperardeLaBase(data, delegacion)
    End Function

    Public Shared Function CalcularGMT(fechaHora As DateTime, identificadorAjeno As String, codigoDelegacionOrigen As String, codigoDelegacionDestino As String) As DateTime

        If fechaHora = DateTime.MinValue Then
            fechaHora = Now
        End If

        'Hora Atual (GMT Zero)
        Dim fechaHoraCalculada = New DateTime(fechaHora.Year, fechaHora.Month, fechaHora.Day, DateTime.UtcNow.Hour, DateTime.UtcNow.Minute, DateTime.UtcNow.Second)

        Dim esGMTInformado As Boolean = Not String.IsNullOrEmpty(fechaHora.ToString("%K"))

        Dim GMTHoraLocalCalculado As Integer = Convert.ToInt32(fechaHora.ToString("zzz").Split(":")(0)) * -1
        Dim GMTMinutoLocalCalculado As Integer = Convert.ToInt32(fechaHora.ToString("zzz").Split(":")(1)) * -1

        If esGMTInformado Then

            fechaHoraCalculada = fechaHora.AddHours(GMTHoraLocalCalculado).AddMinutes(GMTMinutoLocalCalculado)

        Else 'If fechaHora.Hour > 0 OrElse fechaHora.Minute > 0 OrElse fechaHora.Second > 0 Then

            If String.IsNullOrEmpty(codigoDelegacionOrigen) AndAlso String.IsNullOrEmpty(codigoDelegacionDestino) Then

                Dim d As New Contractos.Integracion.Comon.Detalle
                AccesoDatos.Util.resultado(d.Codigo,
                           d.Descripcion,
                           Comon.Enumeradores.Mensajes.Tipo.Error_Negocio,
                           Comon.Enumeradores.Mensajes.Contexto.Integraciones,
                           Comon.Enumeradores.Mensajes.Funcionalidad.General,
                           "0022", fechaHora.ToString("dd/MM/yyyy hh:mm:ss"), True)

                Throw New Excepcion.NegocioExcepcion(d.Codigo, d.Descripcion)

            End If

            fechaHoraCalculada = fechaHora

            fechaHoraCalculada = ObtenerFechaHoraGMT(codigoDelegacionOrigen, codigoDelegacionDestino, identificadorAjeno, fechaHora, True)
        End If

        Return fechaHoraCalculada
    End Function

    Private Shared Function ObtenerFechaHoraGMT(codigoDelegacionOrigen As String, codigoDelegacionDestino As String, IdentificadorAjeno As String, fechaHora As DateTime, esGMTZero As Boolean) As DateTime

        Dim _delegacion As Clases.Delegacion
        Dim codigoDelegacion As String = codigoDelegacionOrigen

        If Not String.IsNullOrEmpty(codigoDelegacionDestino) Then
            codigoDelegacion = codigoDelegacionDestino
        End If

        _delegacion = AccesoDatos.Genesis.Delegacion.ObtenerDelegacionGMT(codigoDelegacion, IdentificadorAjeno)

        ' Delegacion
        If _delegacion Is Nothing Then
            Throw New Excepcion.NegocioExcepcion(String.Format(Tradutor.Traduzir("msg_delegacion_vazio"), codigoDelegacion) & If(String.IsNullOrEmpty(IdentificadorAjeno), "", " Identificador ajeno: " & IdentificadorAjeno))
        End If

        If esGMTZero Then
            Return Util.DataHoraGMT_GrabarEnLaBase(fechaHora, _delegacion)
        Else
            Return Util.DataHoraGMT_RecuperardeLaBase(fechaHora, _delegacion)
        End If

    End Function

#Region "[Calcular GMT]"

    Public Shared Sub CalcularGMT(fechaHora As DateTime,
                                  codigoDelegacion As String,
                                  identificadorAjeno As String,
                                  ByRef GMTHoraLocalCalculado As Double,
                                  ByRef GMTMinutoLocalCalculado As Double)

        If fechaHora = DateTime.MinValue Then
            fechaHora = Now
        End If

        GMTHoraLocalCalculado = Convert.ToInt32(fechaHora.ToString("zzz").Split(":")(0))
        GMTMinutoLocalCalculado = Convert.ToInt32(fechaHora.ToString("zzz").Split(":")(1))

    End Sub

#End Region

    Public Shared Function ValidarToken(Peticion As Object,
                                        ByRef Resultado As Contractos.Integracion.Comon.Resultado) As Boolean

        Try

            If Peticion Is Nothing Then
                Throw New Excepcion.NegocioExcepcion("6", "")
            Else
                If AppSettings("Token") IsNot Nothing AndAlso Not String.IsNullOrEmpty(AppSettings("Token")) Then
                    If Peticion.Configuracion Is Nothing OrElse String.IsNullOrEmpty(Peticion.Configuracion.Token) OrElse Not AppSettings("Token").Equals(Peticion.Configuracion.Token) Then
                        If Peticion.Configuracion Is Nothing OrElse String.IsNullOrEmpty(Peticion.Configuracion.Token) Then
                            Throw New Excepcion.NegocioExcepcion("7", String.Empty)
                        Else
                            Throw New Excepcion.NegocioExcepcion("7", Peticion.Configuracion.Token)
                        End If
                    End If
                End If
            End If

        Catch ex As Excepcion.NegocioExcepcion

            If Resultado.Detalles Is Nothing Then Resultado.Detalles = New List(Of Contractos.Integracion.Comon.Detalle)
            Dim d As New Contractos.Integracion.Comon.Detalle
            AccesoDatos.Util.resultado(d.Codigo,
                       d.Descripcion,
                       Enumeradores.Mensajes.Tipo.Error_Negocio,
                       Enumeradores.Mensajes.Contexto.Integraciones,
                       Enumeradores.Mensajes.Funcionalidad.General,
                       "000" & ex.Codigo, ex.Descricao, True)
            Resultado.Detalles.Add(d)

            Return False

        Catch ex As Exception

            AccesoDatos.Util.resultado(Resultado.Codigo,
                           Resultado.Descripcion,
                           Enumeradores.Mensajes.Tipo.Error_Aplicacion,
                           Enumeradores.Mensajes.Contexto.Integraciones,
                           Enumeradores.Mensajes.Funcionalidad.General,
                           "0000", "",
                           True)

            If Resultado.Detalles Is Nothing Then Resultado.Detalles = New List(Of Contractos.Integracion.Comon.Detalle)
            Dim detalle As New Contractos.Integracion.Comon.Detalle
            AccesoDatos.Util.resultado(detalle.Codigo,
                               detalle.Descripcion,
                               Enumeradores.Mensajes.Tipo.Error_Aplicacion,
                               Enumeradores.Mensajes.Contexto.Integraciones,
                               Enumeradores.Mensajes.Funcionalidad.General,
                               "0001", Util.RecuperarMensagemTratada(ex),
                               True)
            Resultado.Detalles.Add(detalle)

            Return False

        End Try

        Return True

    End Function

    Public Shared Function obtenerURLLegado(codigoDelegacion As String) As String

        Dim resp As String = String.Empty

        Dim listParametros As New List(Of String)
        listParametros.Add(Comon.Constantes.CODIGO_PARAMETRO_SALDOS_PAIS_URL_SERVICIO_PERIODO)

        Dim parametros As List(Of Clases.Parametro) = AccesoDatos.Genesis.Parametros.ObtenerParametrosDelegacionPais(codigoDelegacion, Comon.Constantes.CODIGO_APLICACION_GENESIS_SALDOS, listParametros)
        If parametros IsNot Nothing Then
            For Each parametro In parametros
                If parametro.codigoParametro = Comon.Constantes.CODIGO_PARAMETRO_SALDOS_PAIS_URL_SERVICIO_PERIODO Then
                    resp = parametro.valorParametro
                End If
            Next
        End If

        If String.IsNullOrEmpty(resp) Then
            resp = "ERROR"
        ElseIf resp.ToLower().StartsWith("www.") Then
            resp = "http://" & resp
        End If

        Return resp
    End Function

    Public Shared Function TransformaGMT_Zero(data As DateTime) As DateTime

        Dim GMTHoraLocalCalculadoDesde As Integer = Convert.ToInt32(data.ToString("zzz").Split(":")(0)) * -1
        Dim GMTMinutoLocalCalculadoDesde As Integer = Convert.ToInt32(data.ToString("zzz").Split(":")(1)) * -1

        Return data.AddHours(GMTHoraLocalCalculadoDesde).AddMinutes(GMTMinutoLocalCalculadoDesde)


    End Function
End Class
