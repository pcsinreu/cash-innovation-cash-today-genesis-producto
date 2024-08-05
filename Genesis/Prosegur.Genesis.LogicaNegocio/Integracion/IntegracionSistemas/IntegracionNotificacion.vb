Imports Prosegur.Genesis.Comunicacion
Imports Prosegur.Genesis.Comunicacion.ProxyWS.WebApi.HttpUtil
Imports Prosegur.Genesis.ContractoServicio
Imports Prosegur.Genesis.Utilidad

Public Class IntegracionNotificacion
    Inherits BaseIntegracion

    Public Sub New(usuario As String, CodigoProcesoNotificacion As String, CodigoPais As String, IdentificadorLlamada As String, identificadorAjeno As String)
        MyBase.New(New Contractos.Integracion.IntegracionSistemas.Integracion.Configuracion With {
            .CodigoProceso = CodigoProcesoNotificacion,
            .SistemaOrigem = Constantes.CONST_SISTEMA_GENESIS_PRODUCTO,
            .SistemaDestino = Constantes.CONST_SISTEMA_API_GLOBAL,
            .NombreParametroReintentoMaximo = Constantes.CONST_PARAM_REINTENTO_API_GLOBAL,
            .EstadosBusquedaIntegracion = New List(Of Comon.Enumeradores.EstadoIntegracion)({Comon.Enumeradores.EstadoIntegracion.Abierto, Comon.Enumeradores.EstadoIntegracion.Modificado}),
            .Usuario = usuario,
            .CodigoAplicacion = Comon.Constantes.CODIGO_APLICACION_GENESIS,
            .NombreParametroUrl = Comon.Constantes.CODIGO_PARAMETRO_URL_NOTIFICACION_API_GLOBAL,
            .CodigoPais = CodigoPais,
            .IdentificadorLlamada = IdentificadorLlamada,
            .IdentificadorAjeno = identificadorAjeno
            }
        )
    End Sub

    Private Function GetToken() As String
        Dim parametros = New Dictionary(Of String, String) From {
            {Comon.Constantes.CONST_CLIENT_ID, GetParametro(Comon.Constantes.CODIGO_APLICACION_GENESIS, Comon.Constantes.CODIGO_PARAMETRO_URL_AUTENTICACION_API_GLOBAL_CI)},
            {Comon.Constantes.CONST_CLIENT_SECRET, GetParametro(Comon.Constantes.CODIGO_APLICACION_GENESIS, Comon.Constantes.CODIGO_PARAMETRO_URL_AUTENTICACION_API_GLOBAL_CS)},
            {Comon.Constantes.CONST_SCOPE, GetParametro(Comon.Constantes.CODIGO_APLICACION_GENESIS, Comon.Constantes.CODIGO_PARAMETRO_URL_AUTENTICACION_API_GLOBAL_SCOPE)},
            {Comon.Constantes.CONST_GRANT_TYPE, Comon.Constantes.CONST_CLIENT_CREDENTIALS}
        }
        Dim UrlAuth = GetParametro(Comon.Constantes.CODIGO_APLICACION_GENESIS, Comon.Constantes.CODIGO_PARAMETRO_URL_AUTENTICACION_API_GLOBAL)

        Return TokensModule.BuscarTokenBearerConClientCredencial("Notificacion", UrlAuth, parametros, Configuracion.IdentificadorLlamada, "Prosegur.Genesis.LogicaNegocio.IntegracionNotificacion.GetToken")


    End Function

    Protected Overrides Function GenerarPeticion(identificadorLlamada As String, identificador As String, codigoPais As String) As Object
        Dim oid_tabla_identificador As String = identificador
        Dim log As New Text.StringBuilder()
        Dim objPeticion As New Contractos.Job.EnviarNotificacion.Peticion With {
            .CodigoPais = Configuracion.CodigoPais,
            .Configuracion = New Contractos.Job.EnviarNotificacion.Entrada.Configuracion With {
            .Usuario = Configuracion.Usuario,
            .IdentificadorAjeno = Configuracion.IdentificadorAjeno
            }
        }

        Dim oid_integracion As String = String.Empty
        Dim unaIntegracion = IntegracionesPendientes.ListaIntegracion.FirstOrDefault(Function(x) x.IdentificadorTablaIntegracion = oid_tabla_identificador)

        If unaIntegracion IsNot Nothing Then
            oid_integracion = unaIntegracion.Identificador
        End If

        Return AccesoDatos.RecuperarNotificaciones.Ejecutar(Configuracion.IdentificadorLlamada, objPeticion, oid_integracion, log)
    End Function

    Protected Overrides Function EjecutarSistemaDestino(identificadorLlamada As String, link As String, peticion As Object) As Object
        Dim proxy = New ProxyWS.WebApi.HttpUtil(Configuracion.CodigoPais, "IntegracionNotificacion")
        Dim headers = New Dictionary(Of String, String)
        headers.Add("AUTHORIZATION", String.Format("Bearer {0}", GetToken()))
        Dim retornoApi As RespuestaHttp(Of Respuesta) = proxy.PostWithHeaders(Of Respuesta)(identificadorLlamada, link, peticion, headers)

        Return retornoApi
    End Function

    Protected Overrides Function ValidarExito(respuesta As Object) As Boolean
        Dim resp = CType(respuesta, RespuestaHttp(Of Respuesta))

        Return resp.StatusCode = "200"
    End Function

    Protected Overrides Function BuscarRespuesta(respuesta As Object) As Integracion
        Dim objResp = New Integracion
        Dim resp = CType(respuesta, RespuestaHttp(Of Respuesta))

        If resp.StatusCode = "200" Then
            objResp.TipoResultado = "0"
            objResp.TipoError = ""
            resp.ReasonPhrase = "OK"
        Else
            objResp.TipoResultado = "2"
            objResp.TipoError = "Status: " & resp.StatusCode & Environment.NewLine &
                                "Reason:" & resp.ReasonPhrase
        End If
        objResp.Detalle = resp.ReasonPhrase

        Return objResp
    End Function

    Public Function ValidarConfiguracionParametro() As Boolean

        Dim urlNotificacion = GetParametro(Comon.Constantes.CODIGO_APLICACION_GENESIS, Comon.Constantes.CODIGO_PARAMETRO_URL_NOTIFICACION_API_GLOBAL)
        Dim UrlAuth = GetParametro(Comon.Constantes.CODIGO_APLICACION_GENESIS, Comon.Constantes.CODIGO_PARAMETRO_URL_AUTENTICACION_API_GLOBAL)
        Dim scope = GetParametro(Comon.Constantes.CODIGO_APLICACION_GENESIS, Comon.Constantes.CODIGO_PARAMETRO_URL_AUTENTICACION_API_GLOBAL_SCOPE)
        Dim ClientSecret = GetParametro(Comon.Constantes.CODIGO_APLICACION_GENESIS, Comon.Constantes.CODIGO_PARAMETRO_URL_AUTENTICACION_API_GLOBAL_CS)
        Dim ClientId = GetParametro(Comon.Constantes.CODIGO_APLICACION_GENESIS, Comon.Constantes.CODIGO_PARAMETRO_URL_AUTENTICACION_API_GLOBAL_CI)

        If Not String.IsNullOrWhiteSpace(urlNotificacion) And Not String.IsNullOrWhiteSpace(UrlAuth) And Not String.IsNullOrWhiteSpace(scope) And Not String.IsNullOrWhiteSpace(ClientSecret) And Not String.IsNullOrWhiteSpace(ClientId) Then
            Return True
        Else
            Return False
        End If

    End Function
    Public Class Respuesta
        Public Property Codigo As String
        Public Property Descripcion As String
    End Class
End Class
