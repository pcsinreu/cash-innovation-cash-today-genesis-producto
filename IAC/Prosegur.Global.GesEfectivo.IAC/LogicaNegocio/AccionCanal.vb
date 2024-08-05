Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Canal
Imports Prosegur.Global.GesEfectivo.IAC
Imports Prosegur.DbHelper
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Global.GesEfectivo.IAC.AccesoDatos
Imports Prosegur.Genesis

Public Class AccionCanal
    Implements ContractoServicio.ICanal

    ''' <summary>
    '''Metodo faz a busca dos canais.
    ''' </summary>
    ''' <param name="peticion"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 15/01/2009 Created
    ''' </history>
    Public Function GetCanales(Peticion As ContractoServicio.Canal.GetCanales.Peticion) As ContractoServicio.Canal.GetCanales.Respuesta Implements ContractoServicio.ICanal.getCanales

        Dim objRespuesta As New ContractoServicio.Canal.GetCanales.Respuesta

        Try

            objRespuesta.Canales = AccesoDatos.Canal.GetCanales(Peticion)
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
    '''Metodo faz a busca dos canais e dos seus subcanais.
    ''' </summary>
    ''' <param name="peticion"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 15/01/2009 Created
    ''' </history>
    Public Function GetSubCanalesByCanal(Peticion As ContractoServicio.Canal.GetSubCanalesByCanal.Peticion) As ContractoServicio.Canal.GetSubCanalesByCanal.Respuesta Implements ContractoServicio.ICanal.getSubCanalesByCanal

        Dim objRespuesta As New ContractoServicio.Canal.GetSubCanalesByCanal.Respuesta

        Try

            objRespuesta.Canales = AccesoDatos.SubCanal.GetSubCanalesByCanal(Peticion)
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
    ''' a nivel de canal e subcanal.
    ''' </summary>
    ''' <param name="peticion"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 15/01/2009 Created
    ''' [octavio.piramo] 10/03/2009 Alterado
    ''' </history>
    Public Function SetCanales(Peticion As ContractoServicio.Canal.SetCanal.Peticion) As ContractoServicio.Canal.SetCanal.Respuesta Implements ContractoServicio.ICanal.setCanales

        ' criar objeto respuesta
        Dim objRespuesta As New ContractoServicio.Canal.SetCanal.Respuesta
        objRespuesta.RespuestaCanales = New ContractoServicio.Canal.SetCanal.RespuestaCanalColeccion

        Dim temErro As Boolean = False
        Dim OidCanales As String = String.Empty

        ' percorrer todos os canais
        For Each cn As ContractoServicio.Canal.SetCanal.Canal In Peticion.Canales

            Dim objRespuestaCanal As New SetCanal.RespuestaCanal
            objRespuestaCanal.CodigoCanal = cn.Codigo
            objRespuestaCanal.DescripcionCanal = cn.Descripcion

            Try

                ' Verificar se codigo canal foi enviado
                If cn.Codigo Is Nothing OrElse String.IsNullOrEmpty(cn.Codigo) Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("001_msg_CanalCodigoVazio"))
                End If

                ' obter oid canal
                Dim oidCanal As String = IAC.AccesoDatos.Canal.BuscaOidCanal(cn.Codigo)

                Dim objTransacion = New Transacao(AccesoDatos.Constantes.CONEXAO_GE)

                ' caso oid seja encontrado
                If oidCanal <> String.Empty Then

                    ' se o canal não for vigente e estiver sendo utilizado por algum processo.
                    If Not cn.Vigente AndAlso IAC.AccesoDatos.Canal.verificarSiPoseeProcesoVigente(cn.Codigo) Then
                        ' lançar erro de negócio, canal está sendo usando por um processo.
                        Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("001_msg_CanalProcessoVigente"))
                    Else
                        Dim retornoOidSubCanal As New List(Of KeyValuePair(Of String, ContractoServicio.Canal.SetCanal.SubCanal))

                        ' modificar o canal
                        ModificarCanal(cn, Peticion.CodUsuario, oidCanal, retornoOidSubCanal, objTransacion)

                        objRespuestaCanal.CodigosAjenos = InsertCodigoAJenoCanal(cn, oidCanal, objTransacion)

                        For Each item In retornoOidSubCanal
                            InsertCodigoAJenoSubCanal(item.Value, item.Key, Peticion.CodUsuario, cn.gmtCreacion, cn.gmtModificacion, objTransacion)
                        Next

                    End If
                Else

                    'verifica se a descrição não e nula/vazia.
                    If cn.Descripcion Is Nothing OrElse String.IsNullOrEmpty(cn.Descripcion) Then
                        Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("001_msg_CanalDescripcion"))
                    End If

                    Dim retornoOidSubCanal As New List(Of KeyValuePair(Of String, ContractoServicio.Canal.SetCanal.SubCanal))

                    'insere o canal e se tiver subcanais faz a inserção tambem
                    OidCanales = IAC.AccesoDatos.Canal.AltaCanal(cn, Peticion.CodUsuario, retornoOidSubCanal, objTransacion)

                    objRespuestaCanal.CodigosAjenos = InsertCodigoAJenoCanal(cn, OidCanales, objTransacion)

                    For Each item In retornoOidSubCanal
                        InsertCodigoAJenoSubCanal(item.Value, item.Key, Peticion.CodUsuario, cn.gmtCreacion, cn.gmtModificacion, objTransacion)
                    Next

                End If

                'Realiza transações
                objTransacion.RealizarTransacao()

                objRespuestaCanal.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
                objRespuestaCanal.MensajeError = String.Empty

            Catch ex As Excepcion.NegocioExcepcion

                'Caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada
                objRespuestaCanal.CodigoError = ex.Codigo
                objRespuestaCanal.MensajeError = ex.Descricao
                temErro = False

            Catch ex As Exception
                Util.TratarErroBugsnag(ex)

                'Caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
                objRespuestaCanal.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
                objRespuestaCanal.MensajeError = ex.ToString()
                temErro = True

            Finally
                objRespuesta.RespuestaCanales.Add(objRespuestaCanal)
            End Try

        Next

        If temErro Then
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT

            ' Recupera os erros que são de ambiente
            Dim errosCanal = From ra As SetCanal.RespuestaCanal In objRespuesta.RespuestaCanales _
                                 Where ra.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT

            If errosCanal IsNot Nothing AndAlso errosCanal.Count > 0 Then
                objRespuesta.MensajeError = errosCanal.First.MensajeError
            Else
                objRespuesta.MensajeError = Traduzir("001_msg_ErroCollecionCanales")
            End If

            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD
        Else
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = String.Empty
        End If

        Return objRespuesta

    End Function

    ''' <summary>
    '''Metodo verifica se o canal informado existe.
    ''' </summary>
    ''' <param name="peticion"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 15/01/2009 Created
    ''' </history>
    Public Function VerificarCodigoCanal(Peticion As ContractoServicio.Canal.VerificarCodigoCanal.Peticion) As ContractoServicio.Canal.VerificarCodigoCanal.Respuesta Implements ContractoServicio.ICanal.VerificarCodigoCanal
        Dim objRespuesta As New ContractoServicio.Canal.VerificarCodigoCanal.Respuesta

        Try

            objRespuesta.Existe = AccesoDatos.Canal.VerificarCodigoCanal(Peticion.Codigo)
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
    '''Metodo verifica se a descrição informada existe.
    ''' </summary>
    ''' <param name="peticion"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 15/01/2009 Created
    ''' </history>
    Public Function VerificarDescripcionCanal(Peticion As ContractoServicio.Canal.VerificarDescripcionCanal.Peticion) As ContractoServicio.Canal.VerificarDescripcionCanal.Respuesta Implements ContractoServicio.ICanal.VerificarDescripcionCanal
        Dim objRespuesta As New ContractoServicio.Canal.VerificarDescripcionCanal.Respuesta

        Try

            objRespuesta.Existe = AccesoDatos.Canal.VerificarDescripcionCanal(Peticion.Descripcion)
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
    '''Metodo verifica se o subcanal informado existe.
    ''' </summary>
    ''' <param name="peticion"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 15/01/2009 Created
    ''' </history>
    Public Function VerificarCodigoSubCanal(Peticion As ContractoServicio.Canal.VerificarCodigoSubCanal.Peticion) As ContractoServicio.Canal.VerificarCodigoSubCanal.Respuesta Implements ContractoServicio.ICanal.VerificarCodigoSubCanal
        Dim objRespuesta As New ContractoServicio.Canal.VerificarCodigoSubCanal.Respuesta

        Try

            objRespuesta.Existe = AccesoDatos.SubCanal.VerificarCodigoSubCanal(Peticion.Codigo)
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
    '''Metodo verifica se a descrição de canal informada existe.
    ''' </summary>
    ''' <param name="peticion"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 15/01/2009 Created
    ''' </history>
    Public Function VerificarDescripcionSubCanal(Peticion As ContractoServicio.Canal.VerificarDescripcionSubCanal.Peticion) As ContractoServicio.Canal.VerificarDescripcionSubCanal.Respuesta Implements ContractoServicio.ICanal.VerificarDescripcionSubCanal
        Dim objRespuesta As New ContractoServicio.Canal.VerificarDescripcionSubCanal.Respuesta

        Try

            objRespuesta.Existe = AccesoDatos.SubCanal.VerificarDescripcionSubCanal(Peticion.Descripcion)
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
    ''' Metodo responsaval por fazer o update, exclusão ou inserção de canais.
    ''' </summary>
    ''' <param name="objCanal"></param>
    ''' <param name="CodigoUsuario"></param>
    ''' <param name="oidCanal"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 15/01/2009 Created
    ''' [octavio.piramo] 10/03/2009 Alterado
    ''' </history>
    Private Sub ModificarCanal(objCanal As ContractoServicio.Canal.SetCanal.Canal, _
                               CodigoUsuario As String, _
                               oidCanal As String,
                               ByRef retornoOidSubCanal As List(Of KeyValuePair(Of String, ContractoServicio.Canal.SetCanal.SubCanal)),
                               ByRef objTransacion As Transacao)

        If retornoOidSubCanal Is Nothing Then
            retornoOidSubCanal = New List(Of KeyValuePair(Of String, ContractoServicio.Canal.SetCanal.SubCanal))
        End If

        ' atualiza o Canal
        IAC.AccesoDatos.Canal.ActualizarCanal(objCanal, CodigoUsuario, objTransacion, oidCanal)

        If objCanal.SubCanales IsNot Nothing _
            AndAlso objCanal.SubCanales.Count > 0 Then

            ' Chamar metodo que Retorna todos os subcanais do canal com o objetivo de ganhar performace
            ' porque não será necessario para subcanal ir ao banco verificar se existe
            Dim objsubCanales As New ContractoServicio.Canal.SetCanal.SubCanalColeccion
            objsubCanales = IAC.AccesoDatos.SubCanal.BuscaTodosSubCanales(objCanal.Codigo)

            ' percorrer todos os subcanais
            For Each sc As IAC.ContractoServicio.Canal.SetCanal.SubCanal In objCanal.SubCanales
                ' verificar se codigo canal foi enviado
                If String.IsNullOrEmpty(sc.Codigo) Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("001_msg_SubCanalCodigoVazio"))
                End If

                ' Utilizando linq verifica em memoria se o subcanal existe no banco
                ' Se existe chama o modificiacion de subcanal
                ' Se não existe chama o insertar de subcanal
                If Not VerificarSubCanalExiste(objsubCanales, sc.Codigo) Then
                    'Insere o Sub Canal
                    Dim oid = SubCanal.AltaSubCanal(sc, CodigoUsuario, oidCanal, objTransacion)
                    Dim item As New KeyValuePair(Of String, ContractoServicio.Canal.SetCanal.SubCanal)(oid, sc)
                    retornoOidSubCanal.Add(item)
                Else

                    ' Se o canal não é vigente já foi verificado se todos os subcanais possuem relacionamento com processo
                    ' Então só é verificado se foi passado canal como vigente
                    If Not sc.Vigente AndAlso IAC.AccesoDatos.SubCanal.VerificarSiPoseerProcesoVigente(sc.Codigo) Then
                        Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("001_msg_SubCanalProcessoVigente"))
                    Else
                        'Atualiza o Sub Canal
                        Dim oid = SubCanal.ActualizarSubCanal(sc, CodigoUsuario, objTransacion)
                        Dim item As New KeyValuePair(Of String, ContractoServicio.Canal.SetCanal.SubCanal)(oid, sc)
                        retornoOidSubCanal.Add(item)
                    End If
                End If
            Next
        End If

        If Not objCanal.Vigente Then
            ' dar baixa em todos os subcanais
            IAC.AccesoDatos.SubCanal.BajaSubCanalNivelCanal(oidCanal, CodigoUsuario, objTransacion)
        End If

    End Sub

    ''' <summary>
    ''' Verifica se SubCanal Existe
    ''' </summary>
    ''' <param name="objsubCanales"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 15/01/2009 Created
    ''' </history>
    Private Function VerificarSubCanalExiste(objsubCanales As IAC.ContractoServicio.Canal.SetCanal.SubCanalColeccion, codigoSubCanal As String) As Boolean

        Dim sc = From c In objsubCanales Where c.Codigo = codigoSubCanal

        If sc Is Nothing OrElse sc.Count = 0 Then
            Return False
        Else
            Return True
        End If

    End Function

    Public Function Test() As ContractoServicio.Test.Respuesta Implements ContractoServicio.ICanal.Test
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


    Public Function InsertCodigoAJenoCanal(objCanal As ContractoServicio.Canal.SetCanal.Canal, oidCanal As String, _
                                            Optional ByRef objTransacion As Transacao = Nothing) As ContractoServicio.CodigoAjeno.SetCodigosAjenos.CodigoAjenoRespuestaColeccion

        Dim objAccionCodigoAjeno As New IAC.LogicaNegocio.AccionCodigoAjeno
        Dim objRespuesta As New ContractoServicio.CodigoAjeno.SetCodigosAjenos.Respuesta

        If objCanal.CodigoAjeno IsNot Nothing Then
            Dim objCodigoAjeno As New ContractoServicio.CodigoAjeno.SetCodigosAjenos.Peticion
            objCodigoAjeno.CodigosAjenos = New ContractoServicio.CodigoAjeno.SetCodigosAjenos.CodigoAjenoColeccion
            For Each item In objCanal.CodigoAjeno
                Dim objItemCast As New ContractoServicio.CodigoAjeno.SetCodigosAjenos.CodigoAjeno
                Dim codigoTipoTabla As String = String.Empty

                objItemCast.OidTablaGenesis = oidCanal
                objItemCast.OidCodigoAjeno = item.OidCodigoAjeno
                objItemCast.BolActivo = item.BolActivo
                objItemCast.BolDefecto = item.BolDefecto
                objItemCast.CodAjeno = item.CodAjeno
                objItemCast.CodIdentificador = item.CodIdentificador
                objItemCast.DesAjeno = item.DesAjeno
                objItemCast.DesUsuarioCreacion = objCanal.desUsuarioCreacion
                objItemCast.DesUsuarioModificacion = objCanal.desUsuarioModificacion
                objItemCast.GmtCreacion = objCanal.gmtCreacion
                objItemCast.GmtModificacion = objCanal.gmtModificacion
                codigoTipoTabla = ContractoServicio.Constantes.COD_CANAL
                objItemCast.CodTipoTablaGenesis = (From iten In Constantes.MapeoEntidadesCodigoAjeno
                                                    Where iten.CodTipoTablaGenesis = codigoTipoTabla
                                                    Select iten.Entidade).FirstOrDefault()


                objCodigoAjeno.CodigosAjenos.Add(objItemCast)
                'CodigoAjeno.SetCodigosAjenos(item)
            Next
            'Só objCodigoAjeno.CodigosAjenos 
            If objCodigoAjeno.CodigosAjenos IsNot Nothing Then
                objRespuesta = objAccionCodigoAjeno.SetCodigosAjenos(objCodigoAjeno, objTransacion)
            End If

        End If

        Return objRespuesta.CodigosAjenos
    End Function

    Public Function InsertCodigoAJenoSubCanal(objSubCanal As ContractoServicio.Canal.SetCanal.SubCanal, oidSubCanal As String, codigoUsuario As String, _
                                             DataCreacion As DateTime, DataModificacion As DateTime, _
                                            Optional ByRef objTransacion As Transacao = Nothing) As ContractoServicio.CodigoAjeno.SetCodigosAjenos.CodigoAjenoRespuestaColeccion

        Dim objAccionCodigoAjeno As New IAC.LogicaNegocio.AccionCodigoAjeno
        Dim objRespuesta As New ContractoServicio.CodigoAjeno.SetCodigosAjenos.Respuesta

        If objSubCanal.CodigoAjeno IsNot Nothing Then
            Dim objCodigoAjeno As New ContractoServicio.CodigoAjeno.SetCodigosAjenos.Peticion
            objCodigoAjeno.CodigosAjenos = New ContractoServicio.CodigoAjeno.SetCodigosAjenos.CodigoAjenoColeccion
            For Each item In objSubCanal.CodigoAjeno
                Dim objItemCast As New ContractoServicio.CodigoAjeno.SetCodigosAjenos.CodigoAjeno
                Dim codigoTipoTabla As String = String.Empty

                objItemCast.OidTablaGenesis = oidSubCanal
                objItemCast.OidCodigoAjeno = item.OidCodigoAjeno
                objItemCast.BolActivo = item.BolActivo
                objItemCast.BolDefecto = item.BolDefecto
                objItemCast.CodAjeno = item.CodAjeno
                objItemCast.CodIdentificador = item.CodIdentificador
                objItemCast.DesAjeno = item.DesAjeno
                objItemCast.DesUsuarioModificacion = codigoUsuario
                objItemCast.GmtModificacion = DataCreacion
                objItemCast.DesUsuarioCreacion = codigoUsuario
                objItemCast.GmtCreacion = DataCreacion

                codigoTipoTabla = ContractoServicio.Constantes.COD_SUBCANAL
                objItemCast.CodTipoTablaGenesis = (From iten In Constantes.MapeoEntidadesCodigoAjeno
                                                    Where iten.CodTipoTablaGenesis = codigoTipoTabla
                                                    Select iten.Entidade).FirstOrDefault()


                objCodigoAjeno.CodigosAjenos.Add(objItemCast)
                'CodigoAjeno.SetCodigosAjenos(item)           
            Next
            If objCodigoAjeno.CodigosAjenos IsNot Nothing Then
                objRespuesta = objAccionCodigoAjeno.SetCodigosAjenos(objCodigoAjeno, objTransacion)
            End If
        End If

        Return objRespuesta.CodigosAjenos
    End Function

    Public Function GetSubCanalesByCertificado(Peticion As ContractoServicio.Canal.GetSubCanalesByCertificado.Peticion) As ContractoServicio.Canal.GetSubCanalesByCertificado.Respuesta Implements ContractoServicio.ICanal.GetSubCanalesByCertificado
        Dim objRespuesta As New ContractoServicio.Canal.GetSubCanalesByCertificado.Respuesta

        Try
            objRespuesta = AccesoDatos.SubCanal.GetSubCanalesByCertificado(Peticion)
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
End Class
