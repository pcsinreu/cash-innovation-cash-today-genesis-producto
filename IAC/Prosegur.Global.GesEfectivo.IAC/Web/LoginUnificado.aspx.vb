Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio
Imports Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio
Imports Prosegur.Genesis.ContractoServicio
Imports Prosegur.Genesis.ContractoServicio.Login
Imports System.Reflection
Imports Prosegur.Genesis.Web.Login
Imports Prosegur.Genesis
Imports Prosegur.Genesis.Comunicacion
Imports Newtonsoft.Json

Public Class LoginUnificado
    Inherits Base


    Protected Overrides Sub DefinirParametrosBase()
        MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Inicial
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.LOGIN
        MyBase.ValidarAcesso = False
        MyBase.ValidarAcao = False
    End Sub

    ''' <summary>
    ''' Inicialização da página de login unificado responsãvel por receber a token e converter as permisos para a sesion atual
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub Inicializar()

        Dim _IdentificadorLlamada As String = String.Empty
        Try

            Prosegur.Genesis.LogicaNegocio.Genesis.Log.GenerarIdentificador("IACLoginUnificado", _IdentificadorLlamada)
            Genesis.LogicaNegocio.Genesis.Log.Iniciar("IACLoginUnificado", "Inicializar", _IdentificadorLlamada)

            Master.HabilitarMenu = False
            Master.MostrarCabecalho = True
            Master.HabilitarHistorico = False
            Master.MostrarRodape = True
            Master.MenuRodapeVisivel = False


            If Request.QueryString("Salir") IsNot Nothing Then
                ' mata a session do usuário
                MyBase.InformacionUsuario = Nothing
                ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType(), Guid.NewGuid.ToString,
                    String.Format("alert('{0}'); window.close();", Traduzir("007_msg_sessao_finalizada")), True)

                Prosegur.Genesis.LogicaNegocio.Genesis.Log.AgregaDetalle(_IdentificadorLlamada,
                                                                     "Prosegur.Global.GesEfectivo.IAC.Web.Inicializar",
                                                                     $"Sesión finalizada",
                                                                     "")
                Return
            End If

            If Request.QueryString("SinPermisos") IsNot Nothing Then
                MyBase.MostraMensagem(Traduzir("007_msg_usuario_sem_permisio"))
                Prosegur.Genesis.LogicaNegocio.Genesis.Log.AgregaDetalle(_IdentificadorLlamada,
                                                                     "Prosegur.Global.GesEfectivo.IAC.Web.Inicializar",
                                                                     $"Sin permisos",
                                                                     "")

                Return
            End If

            If Request.QueryString("SesionExpirada") IsNot Nothing Then
                MyBase.MostraMensagem(Traduzir("007_msg_usuario_sessao_expirada"))
                Prosegur.Genesis.LogicaNegocio.Genesis.Log.AgregaDetalle(_IdentificadorLlamada,
                                                                     "Prosegur.Global.GesEfectivo.IAC.Web.Inicializar",
                                                                     $"Sesion expirada",
                                                                     "")
                Return
            End If

            ' pega a versão

            Dim codVersion = Prosegur.Genesis.Comon.Util.VersionPunto
            'Dim codVersion = Prosegur.Genesis.Web.Login.TokenUtil.ObtenerVersion(Assembly.GetExecutingAssembly)
            Prosegur.Genesis.LogicaNegocio.Genesis.Log.AgregaDetalle(_IdentificadorLlamada,
                                                                     "Prosegur.Global.GesEfectivo.IAC.Web.Inicializar",
                                                                     "Previo llamada a ConsumirToken con token: " & Page.Request("ta"),
                                                                     "")
            ' Ao consumir a token as permisos são retornadas
            Dim tokenConsumida = Prosegur.Genesis.LogicaNegocio.AccionConsumirTokenAcceso.ConsumirToken(Page.Request("ta"), Page.Request.UserAgent, Page.Request.UserHostAddress,
                                                                Prosegur.Genesis.Comon.Constantes.CODIGO_APLICACION_GENESIS_IAC,
                                                                codVersion, _IdentificadorLlamada)

            Prosegur.Genesis.LogicaNegocio.Genesis.Log.AgregaDetalle(_IdentificadorLlamada,
                                                                     "Prosegur.Global.GesEfectivo.IAC.Web.Inicializar",
                                                                     $"Despues de pasar la llamada del token ConsumirToken tokenConsumida: { tokenConsumida }",
                                                                     "")

            ' se a token tiver sido consumida
            If tokenConsumida Then

                ' convertemos as permisos genericas do seguridad para as permisos corretas da aplicação
                MyBase.InformacionUsuario = ConverterPermissoes(Parametros.Permisos)
                MyBase.LoginUsuario = Parametros.Permisos.Usuario.Login.ToUpper

                Prosegur.Genesis.LogicaNegocio.Genesis.Log.AgregaDetalle(_IdentificadorLlamada,
                                                                     "Prosegur.Global.GesEfectivo.IAC.Web.Inicializar",
                                                                     $"Previo GuardarDelegacion",
                                                                     "")
                ' Guarda a delegacion seleciona na sessao atual
                GuardarDelegacion(Parametros.Permisos)
                Prosegur.Genesis.LogicaNegocio.Genesis.Log.AgregaDetalle(_IdentificadorLlamada,
                                                                     "Prosegur.Global.GesEfectivo.IAC.Web.Inicializar",
                                                                     $"Previo CarregarParametroIAC",
                                                                     "")
                CarregarParametroIAC()

                Prosegur.Genesis.LogicaNegocio.Genesis.Log.AgregaDetalle(_IdentificadorLlamada,
                                                                     "Prosegur.Global.GesEfectivo.IAC.Web.Inicializar",
                                                                     "Finaliza llamada Inicializar OK",
                                                                     "")
                Prosegur.Genesis.LogicaNegocio.Genesis.Log.Finalizar(New With {.Codigo = 200, .Descripcion = "Ok"}, _IdentificadorLlamada)
                ' redirecionamos para continuar o fluxo
                Response.Redirect(IAC.ContractoServicio.Constantes.NOME_PAGINA_MENU, False)
            Else
                Prosegur.Genesis.LogicaNegocio.Genesis.Log.Finalizar(New With {.Codigo = 200, .Descripcion = "Ok"}, _IdentificadorLlamada)
                MyBase.MostraMensagem(Traduzir("tkn_token_nao_consumida"))
            End If



        Catch nEx As Excepcion.NegocioExcepcion
            MyBase.MostraMensagemExcecao(nEx)
        Catch ex As Exception

            'Registrar en Log API_LLAMADA
            Dim errorMessage As String = ex.Message
            Dim stackTrace As String = ex.StackTrace

            If ex.InnerException IsNot Nothing Then
                errorMessage += Environment.NewLine + "Inner Exception: " + ex.InnerException.Message
                stackTrace += Environment.NewLine + "Inner Exception Stack Trace: " + ex.InnerException.StackTrace
            End If

            Prosegur.Genesis.LogicaNegocio.Genesis.Log.AgregaDetalle(_IdentificadorLlamada,
                                                                     "Prosegur.Global.GesEfectivo.IAC.Web.Inicializar",
                                                                     "errorMessage " & errorMessage,
                                                                     "")
            Prosegur.Genesis.LogicaNegocio.Genesis.Log.AgregaDetalle(_IdentificadorLlamada,
                                                                     "Prosegur.Global.GesEfectivo.IAC.Web.Inicializar",
                                                                     "stackTrace " & stackTrace,
                                                                     "")
            Prosegur.Genesis.LogicaNegocio.Genesis.Log.Finalizar(New With {.Codigo = 500, .Descripcion = ex.Message}, _IdentificadorLlamada)
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub


    Private Sub CarregarParametroIAC()

        Dim objPeticion As New GetParametrosDelegacionPais.Peticion
        objPeticion.CodigoAplicacion = Prosegur.Genesis.Comon.Constantes.CODIGO_APLICACION_GENESIS
        objPeticion.CodigoDelegacion = Parametros.Permisos.Usuario.Continentes(0).Paises(0).Delegaciones(0).Codigo
        objPeticion.ValidarParametros = True


        Dim ProxyIntegracion As New ProxyIacIntegracion
        Dim objRespuesta As Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio.GetParametrosDelegacionPais.Respuesta = ProxyIntegracion.GetParametrosDelegacionPais(objPeticion)
        ' Recupera os parâmetros da delegação
        If objRespuesta IsNot Nothing AndAlso objRespuesta.CodigoError <> Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT Then
            Throw New Excepcion.NegocioExcepcion(Prosegur.Genesis.Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_ATENCION, objRespuesta.MensajeError)
        End If

        ' Se existe parâmetros
        If objRespuesta.Parametros IsNot Nothing AndAlso objRespuesta.Parametros.Count > 0 Then
            Prosegur.Genesis.Parametros.Genesis.Parametros.Inicializar(objRespuesta.Parametros)
        End If

    End Sub
    ''' <summary>
    ''' Converte as permisos da aplicação vindas do seguridad para as permisos corretas da aplicação
    ''' </summary>
    Private Function ConverterPermissoes(permisos As CrearTokenAcceso.Permisos) As ContractoServicio.Login.InformacionUsuario

        Dim InformacionUsuario As New ContractoServicio.Login.InformacionUsuario

        InformacionUsuario.Apelido = permisos.Usuario.Apellido
        InformacionUsuario.Nombre = permisos.Usuario.Nombre



        If permisos.Usuario.Continentes.Count > 0 AndAlso
        permisos.Usuario.Continentes(0).Paises.Count > 0 AndAlso
        permisos.Usuario.Continentes(0).Paises(0).Delegaciones.Count > 0 AndAlso
        permisos.Usuario.Continentes(0).Paises(0).Delegaciones(0).Sectores.Count > 0 Then

            Dim sector = permisos.Usuario.Continentes(0).Paises(0).Delegaciones(0).Sectores(0)
            For Each permiso In sector.Permisos
                ' filtra as permisos da aplicação atual
                If permiso.CodigoAplicacion.Equals(Prosegur.Genesis.Comon.Constantes.CODIGO_APLICACION_GENESIS_IAC) AndAlso
                                    Not InformacionUsuario.Permisos.Contains(permiso.Nombre.ToUpper()) Then
                    InformacionUsuario.Permisos.Add(permiso.Nombre.ToUpper())
                End If
            Next

            For Each role In sector.Roles
                InformacionUsuario.Rol.Add(role.Nombre)
            Next


        End If

        InformacionUsuario.Aplicaciones = Aplicacao.Util.Utilidad.getComboAplicaciones(InformacionUsuario.Permisos)

        Return InformacionUsuario

    End Function

    Private Sub GuardarDelegacion(permisos As CrearTokenAcceso.Permisos)

        Dim DelegacionDic As New Dictionary(Of String, String)
        DelegacionDic.Add(permisos.Usuario.CodigoDelegacion, permisos.Usuario.DesDelegacion)
        DelegacionConectada = DelegacionDic

    End Sub


End Class