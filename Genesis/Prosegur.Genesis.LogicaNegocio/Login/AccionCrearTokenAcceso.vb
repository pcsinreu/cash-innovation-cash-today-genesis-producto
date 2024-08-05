Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.ContractoServicio
Imports Prosegur.Global

Public Class AccionCrearTokenAcceso

    ''' <summary>
    ''' Método responsavel por criar a token de acesso na base do seguridad e retornar o token gerado para o cliente
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function Ejecutar(Peticion As Login.CrearTokenAcceso.Peticion) As Login.CrearTokenAcceso.Respuesta

        Dim respuesta As New Login.CrearTokenAcceso.Respuesta

        Try

            ' valida a petição
            ValidarPeticion(Peticion)

            ' guarda as permisos desta token
            Dim respostaGuardarPermisos = CrearTokenAcceso(Peticion.OidAplicacion, Peticion.Permisos, Peticion.Configuraciones)

            ' valida resposta
            If respostaGuardarPermisos.Codigo <> Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT Then
                Throw New Excepcion.NegocioExcepcion(respostaGuardarPermisos.Codigo, respostaGuardarPermisos.Descripcion)
            End If

            ' se tudo estiver ok geramos a token
            Dim token = Prosegur.Genesis.Web.Login.TokenUtil.GerarToken(
                respostaGuardarPermisos.Token.OidTokenAcceso,
                Peticion.Ip,
                Peticion.BrowserAgent,
                Peticion.UrlServicio)

            ' retorna para o cliente
            respuesta.TokenAcceso = New Login.CrearTokenAcceso.TokenAcceso() With {
                .OidTokenAcceso = respostaGuardarPermisos.Token.OidTokenAcceso,
                .Token = token
            }

        Catch ex As Excepcion.NegocioExcepcion
            respuesta.CodigoError = ex.Codigo
            respuesta.MensajeError = ex.Descricao
        Catch ex As Exception
            Util.TratarErroBugsnag(ex)
            respuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            respuesta.MensajeError = ex.ToString()
        End Try

        Return respuesta

    End Function

    Private Shared Sub ValidarPeticion(Peticion As Login.CrearTokenAcceso.Peticion)

        'Validação comentada para suportar a criação do tokem por webservice chamado de aplicação desktop e abertura do token em uma aplicação web pelo browser
        'If String.IsNullOrEmpty(Peticion.BrowserAgent) Then
        '    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("Gen_msg_atributo_vacio"), Traduzir("tkn_creartokenacceso_browseragent")))
        'End If

        If String.IsNullOrEmpty(Peticion.Ip) Then
            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("Gen_msg_atributo_vacio"), Traduzir("tkn_creartokenacceso_ip")))
        End If

        If String.IsNullOrEmpty(Peticion.OidAplicacion) Then
            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("Gen_msg_atributo_vacio"), Traduzir("tkn_creartokenacceso_codaplicacion")))
        End If

        If Peticion.Permisos Is Nothing Then
            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("Gen_msg_atributo_vacio"), Traduzir("tkn_creartokenacceso_permisos")))
        End If

    End Sub

    ''' <summary>
    ''' Crea un nuevo registro en tabla SAPR_TTOKEN_ACCESO y retorna un OID del nuevo token
    ''' </summary>
    ''' <param name="oidAplicacion"></param>
    ''' <param name="permisos"></param>
    ''' <param name="configuraciones"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function CrearTokenAcceso(oidAplicacion As String, permisos As Login.CrearTokenAcceso.Permisos,
                                             configuraciones As SerializableDictionary(Of String, String)) As Seguridad.ContractoServicio.CrearTokenAcceso.Respuesta

        Dim respuestaSeguridad = New Seguridad.ContractoServicio.CrearTokenAcceso.Respuesta

        Dim objPeticion As New Seguridad.ContractoServicio.CrearTokenAcceso.Peticion
        Dim objProxy As New LoginGlobal.Seguridad()

        objPeticion.OidAplicacion = oidAplicacion
        objPeticion.OidUsuario = permisos.Usuario.OidUsuario
        objPeticion.PermisosSerializados = Prosegur.Genesis.Web.Login.Serializador.Serializar(Of Login.CrearTokenAcceso.Permisos)(permisos)
        objPeticion.ConfiguracionesSerializados = Prosegur.Genesis.Web.Login.Serializador.Serializar(Of SerializableDictionary(Of String, String))(configuraciones)
        'Guardar token en tabla local
        Dim respuesta = Prosegur.Genesis.AccesoDatos.Login.CrearTokenAcceso(objPeticion.OidAplicacion, objPeticion.OidUsuario, objPeticion.PermisosSerializados, objPeticion.ConfiguracionesSerializados)

        If respuesta.CodigoError = 0 Then
            If respuesta.TokenAcceso IsNot Nothing Then
                respuestaSeguridad.Token = New Seguridad.ContractoServicio.CrearTokenAcceso.TokenAcceso With {
                .OidTokenAcceso = respuesta.TokenAcceso.OidTokenAcceso
            }
            Else
                respuestaSeguridad.Codigo = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            End If
        Else
            respuestaSeguridad.Codigo = respuesta.CodigoError
            respuestaSeguridad.Descripcion = respuesta.MensajeError
        End If


        Return respuestaSeguridad
    End Function

End Class
