Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis
Imports Prosegur.Genesis.ContractoServicio.Login
Imports Prosegur.Genesis.ContractoServicio
Imports System.Reflection
Imports Prosegur.Genesis.Web.Login
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Genesis.Comunicacion

Public Class LoginUnificado
    Inherits Base

    Protected Overrides Sub DefinirParametrosBase()
        MyBase.Acao = Enumeradores.eAcao.Inicial
        MyBase.PaginaAtual = Enumeradores.eTelas.LOGIN
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

            Master.HabilitarMenu = False
            Master.HabilitarHistorico = False
            Master.MenuRodapeVisivel = False
            Master.Titulo = Traduzir("023_titulo_pagina")

            Prosegur.Genesis.LogicaNegocio.Genesis.Log.GenerarIdentificador("REPORTELoginUnificado", _IdentificadorLlamada)
            Genesis.LogicaNegocio.Genesis.Log.Iniciar("REPORTELoginUnificado", "Inicializar", _IdentificadorLlamada)

            If Request.QueryString("Salir") IsNot Nothing Then
                'Master.ControleErro.ShowError(Traduzir("002_msg_sessao_finalizada"), False)
                ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType(), Guid.NewGuid.ToString,
                    String.Format("alert('{0}'); window.close();", Traduzir("002_msg_sessao_finalizada")), True)

                Prosegur.Genesis.LogicaNegocio.Genesis.Log.AgregaDetalle(_IdentificadorLlamada,
                                                                     "Prosegur.Global.GesEfectivo.Reportes.Web.Inicializar",
                                                                     $"Sesión finalizada",
                                                                     "")
                Return
            End If

            If Request.QueryString("SinPermisos") IsNot Nothing Then
                MyBase.MostraMensagem(Traduzir("002_msg_usuario_sem_permisio"))
                Prosegur.Genesis.LogicaNegocio.Genesis.Log.AgregaDetalle(_IdentificadorLlamada,
                                                                     "Prosegur.Global.GesEfectivo.Reportes.Web.Inicializar",
                                                                     $"Usuario sin permiso",
                                                                     "")
                Return
            End If

            If Request.QueryString("SesionExpirada") IsNot Nothing Then
                MyBase.MostraMensagem(Traduzir("002_msg_usuario_sessao_expirada"))
                Prosegur.Genesis.LogicaNegocio.Genesis.Log.AgregaDetalle(_IdentificadorLlamada,
                                                                     "Prosegur.Global.GesEfectivo.Reportes.Web.Inicializar",
                                                                     $"Sesión expirado",
                                                                     "")
                Return
            End If

            '#If DEBUG Then
            '            Dim codVersion = "1512.0403"
            '#Else
            '            Dim codVersion = Prosegur.Genesis.Web.Login.TokenUtil.ObtenerVersion(Assembly.GetExecutingAssembly)
            '#End If
            ' pega a versão
            Dim codVersion = Prosegur.Genesis.Comon.Util.VersionPunto
            Prosegur.Genesis.LogicaNegocio.Genesis.Log.AgregaDetalle(_IdentificadorLlamada,
                                                                     "Prosegur.Global.GesEfectivo.Reportes.Web.Inicializar",
                                                                     "Previo llamada a TokenUtil.ConsumirToken con token: " & Page.Request("ta"),
                                                                     "")
            ' Ao consumir a token as permisos são retornadas
            Dim tokenConsumida = Prosegur.Genesis.LogicaNegocio.AccionConsumirTokenAcceso.ConsumirToken(Page.Request("ta"), Page.Request.UserAgent, Page.Request.UserHostAddress,
                                                                                        Constantes.COD_APLICACION,
                                                                                        codVersion, _IdentificadorLlamada)
            Prosegur.Genesis.LogicaNegocio.Genesis.Log.AgregaDetalle(_IdentificadorLlamada,
                                                                     "Prosegur.Global.GesEfectivo.Reportes.Web.Inicializar",
                                                                     $"Despues de pasar la llamada del token ConsumirToken tokenConsumida: { tokenConsumida }",
                                                                     "")

            ' se a token tiver sido consumida
            If tokenConsumida Then

                ' convertemos as permisos genericas do seguridad para as permisos corretas da aplicação
                MyBase.InformacionUsuario = ConverterPermissoes(Parametros.Permisos)
                MyBase.LoginUsuario = Parametros.Permisos.Usuario.Login.ToUpper

                ' Guarda a delegacion seleciona na sessao atual
                GuardarDelegacion(Parametros.Permisos)

                Prosegur.Genesis.LogicaNegocio.Genesis.Log.AgregaDetalle(_IdentificadorLlamada,
                                                                     "Prosegur.Global.GesEfectivo.Reportes.Web.Inicializar",
                                                                     "Finaliza llamada Inicializar OK",
                                                                     "")
                Prosegur.Genesis.LogicaNegocio.Genesis.Log.Finalizar(New With {.Codigo = 200, .Descripcion = "Ok"}, _IdentificadorLlamada)

                ' redirecionamos para continuar o fluxo
                Response.Redireccionar(Constantes.NOME_PAGINA_MENU, False)
            Else
                Prosegur.Genesis.LogicaNegocio.Genesis.Log.Finalizar(New With {.Codigo = 200, .Descripcion = "Ok"}, _IdentificadorLlamada)
                MyBase.MostraMensagem(Traduzir("tkn_token_nao_consumida"))
            End If


        Catch nEx As Excepcion.NegocioExcepcion
            MyBase.MostraMensagem(nEx.Message)

        Catch ex As Exception
            'Registrar en Log API_LLAMADA
            Dim errorMessage As String = ex.Message
            Dim stackTrace As String = ex.StackTrace

            If ex.InnerException IsNot Nothing Then
                errorMessage += Environment.NewLine + "Inner Exception: " + ex.InnerException.Message
                stackTrace += Environment.NewLine + "Inner Exception Stack Trace: " + ex.InnerException.StackTrace
            End If

            Prosegur.Genesis.LogicaNegocio.Genesis.Log.AgregaDetalle(_IdentificadorLlamada,
                                                                     "Prosegur.Global.GesEfectivo.Reportes.Web.Inicializar",
                                                                     "errorMessage " & errorMessage,
                                                                     "")
            Prosegur.Genesis.LogicaNegocio.Genesis.Log.AgregaDetalle(_IdentificadorLlamada,
                                                                     "Prosegur.Global.GesEfectivo.Reportes.Web.Inicializar",
                                                                     "stackTrace " & stackTrace,
                                                                     "")
            Prosegur.Genesis.LogicaNegocio.Genesis.Log.Finalizar(New With {.Codigo = 500, .Descripcion = ex.Message}, _IdentificadorLlamada)
            MyBase.MostraMensagem(ex.Message)
        End Try

    End Sub

    ''' <summary>
    ''' Converte as permisos da aplicação vindas do seguridad para as permisos corretas da aplicação
    ''' </summary>
    Private Function ConverterPermissoes(permisos As CrearTokenAcceso.Permisos) As ContractoServ.Login.InformacionUsuario

        Dim InformacionUsuario As New ContractoServ.Login.InformacionUsuario

        InformacionUsuario.Apelido = permisos.Usuario.Apellido
        InformacionUsuario.Nombre = permisos.Usuario.Nombre


        If permisos.Usuario.Continentes.Count > 0 AndAlso
        permisos.Usuario.Continentes(0).Paises.Count > 0 AndAlso
        permisos.Usuario.Continentes(0).Paises(0).Delegaciones.Count > 0 AndAlso
        DirectCast(permisos.Usuario.Continentes(0).Paises(0).Delegaciones(0), Genesis.ContractoServicio.Login.EjecutarLogin.DelegacionPlanta).Plantas.Count > 0 Then

            Dim delegacion = permisos.Usuario.Continentes(0).Paises(0).Delegaciones(0)

            ' Cria o objeto delegacion do contrato serviço Reportes
            Dim objDelegacion As New Prosegur.Global.GesEfectivo.Reportes.ContractoServ.Login.Delegacion
            objDelegacion.Codigo = delegacion.Codigo
            objDelegacion.Descripcion = delegacion.Nombre

            Dim planta = DirectCast(delegacion, Genesis.ContractoServicio.Login.EjecutarLogin.DelegacionPlanta).Plantas(0)

            Dim objPlanta As New Prosegur.Global.GesEfectivo.Reportes.ContractoServ.Login.Planta

            objPlanta.Codigo = planta.CodigoPlanta
            objPlanta.Descricao = planta.DesPlanta
            objPlanta.Identificador = planta.oidPlanta


            Dim sector = permisos.Usuario.Continentes(0).Paises(0).Delegaciones(0).Sectores(0)
            Dim objTipoSector As New Prosegur.Global.GesEfectivo.Reportes.ContractoServ.Login.TipoSector
            For Each permiso In sector.Permisos
                ' filtra as permisos da aplicação atual
                If permiso.CodigoAplicacion.Equals(Prosegur.Genesis.Comon.Constantes.CODIGO_APLICACION_GENESIS_REPORTES) AndAlso
                                    Not objTipoSector.Permisos.Contains(permiso.Nombre.ToUpper()) Then
                    objTipoSector.Permisos.Add(permiso.Nombre.ToUpper())
                End If
            Next

            For Each role In sector.Roles
                objTipoSector.Rol.Add(role.Nombre)
            Next

            objPlanta.TiposSectores.Add(objTipoSector)
            objDelegacion.Plantas.Add(objPlanta)

            ' adiciona a lista de delegações onde o usuário tem permisão
            InformacionUsuario.Delegaciones.Add(objDelegacion)
            InformacionUsuario.DelegacionLogin = objDelegacion
        End If

        Return InformacionUsuario

    End Function

    Private Sub GuardarDelegacion(permisos As CrearTokenAcceso.Permisos)
        Dim DelegacionDic As New Dictionary(Of String, String)
        DelegacionDic.Add(permisos.Usuario.CodigoDelegacion, permisos.Usuario.DesDelegacion)
        DelegacionConectada = DelegacionDic
    End Sub

End Class