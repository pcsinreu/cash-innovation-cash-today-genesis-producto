Imports System.Threading
Imports System.IO
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Ionic.Zip
Imports System.Runtime.Serialization.Formatters.Binary
Imports System.Text
Imports System.Xml.Serialization
Imports System.Reflection
Imports Microsoft.Web.Services3.Security.Tokens
Imports Prosegur.Genesis.Comunicacion
Imports System.Security.Cryptography
Imports ContractoLogin = Prosegur.Genesis.ContractoServicio.GenesisLogin

Public Class GerenciadorAplicacion

    Public Shared Event TodasAplicacionesCerradas()

#Region "[VARIAVEIS]"

    ' Guarda o diretório corrente
    Private Shared _DiretorioCorrente As String = Environment.CurrentDirectory

    'Diretório onde foram extraídos os arquivos (Aplicação\Versão)
    Private Shared _DiretorioArquivosBaixado As String = String.Empty

    ' Número atual de tentativas de atualização via rede (usado em caso de diferença de checksum)
    Private Shared _TentativasAtualizacaoViaRede = 0

    Private Shared forzarAbrirModulo As Prosegur.Genesis.Comon.Clases.Sector = Nothing

#End Region

#Region "[PROPRIEDADES]"

    ' Guarda os módulos que estão abertos
    Public Shared AplicacionesAbertas As New List(Of ContractoServicio.Login.EjecutarLogin.AplicacionVersion)

    ' Atualizou Genesis
    Public Shared AtualizouGenesis As Boolean = False

#End Region

    ''' <summary>
    ''' Método para carregar versão específica de aplicação
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [prezende]  22/05/2012  Criado
    ''' </history>
    Public Shared Sub CarregarAplicacionVersion(aplicacionVersion As ContractoServicio.Login.EjecutarLogin.AplicacionVersion, Optional telaLogin As Boolean = False, Optional forzarSectorDefecto As Prosegur.Genesis.Comon.Clases.Sector = Nothing)

        Dim caminhoDll As String = String.Empty
        Dim nomeDllAplicacao As String = String.Empty
        Dim formClass As String = String.Empty
        Dim permisoSupervisor As String = String.Empty

        Select Case aplicacionVersion.CodigoAplicacion
            Case Constantes.APP_COD_ATM
                nomeDllAplicacao = Constantes.ATM_DLL_APP
                formClass = Constantes.ATM_FRM_CLASS
                permisoSupervisor = Constantes.ATM_PER_SUPERVISOR
            Case Constantes.APP_COD_CONTEO
                nomeDllAplicacao = Constantes.CONTEO_DLL_APP
                formClass = Constantes.CONTEO_FRM_CLASS
                permisoSupervisor = Constantes.CONTEO_PER_SUPERVISOR
            Case Constantes.APP_COD_EMULADOR
                nomeDllAplicacao = Constantes.EMULADOR_DLL_APP
                formClass = Constantes.EMULADOR_FRM_CLASS
                permisoSupervisor = Constantes.EMULADOR_PER_SUPERVISOR
            Case Constantes.APP_COD_EMULADORATM
                nomeDllAplicacao = Constantes.EMULADORATM_DLL_APP
                formClass = Constantes.EMULADORATM_FRM_CLASS
                permisoSupervisor = Constantes.EMULADORATM_PER_SUPERVISOR
            Case Constantes.APP_COD_SALIDAS
                nomeDllAplicacao = Constantes.SALIDAS_DLL_APP
                formClass = Constantes.SALIDAS_FRM_CLASS
                permisoSupervisor = Constantes.SALIDAS_PER_SUPERVISOR
            Case Constantes.APP_COD_SUPERVISOR_CONTEO
                nomeDllAplicacao = Constantes.SUPERVISOR_CONTEO_DLL_APP
                formClass = Constantes.SUPERVISOR_CONTEO_FRM_CLASS
                permisoSupervisor = Constantes.SUPERVISOR_PER_SUPERVISOR
            Case Constantes.APP_COD_RECEPCION_Y_ENVIO
                    nomeDllAplicacao = Constantes.RECEPCIONYENVIO_DLL_APP
                    formClass = Constantes.RECEPCIONYENVIO_FRM_CLASS
                permisoSupervisor = Constantes.RECEPCIONYENVIO_PER_SUPERVISOR
            Case Constantes.APP_COD_PACK_MODULAR, Constantes.APP_COD_CUSTODIA_PRECINTADA
                nomeDllAplicacao = Constantes.PACKMODULAR_DLL_APP
                formClass = Constantes.PACKMODULAR_FRM_CLASS
                permisoSupervisor = Constantes.PACKMODULAR_PER_SUPERVISOR
            Case Constantes.APP_COD_GENESIS
                nomeDllAplicacao = String.Empty
                formClass = String.Empty
                permisoSupervisor = String.Empty
            Case Constantes.APP_COD_GE_SALIDAS
                nomeDllAplicacao = Constantes.GE_SALIDAS_DLL_APP
                formClass = Constantes.GE_SALIDAS_FRM_CLASS
                permisoSupervisor = Constantes.GE_SALIDAS_PER_SUPERVISOR
        End Select

#If DEBUG Then

        aplicacionVersion.CodigoBuild = "DEBUG"
        aplicacionVersion.DesURLServicio = Parametros.Parametros.URLServicio

#End If

        If Parametros.Parametros.forzarServicio Then
            aplicacionVersion.DesURLServicio = Parametros.Parametros.URLServicio
        End If

        If forzarSectorDefecto IsNot Nothing Then
            Parametros.Parametros.InformacionUsuario.SectorLogado = forzarSectorDefecto
        End If

        ' Recupera o Diretório de onde os arquivos foram baixados e extraídos
        _DiretorioArquivosBaixado = String.Format("{0}\{1}\{2}\", _DiretorioCorrente, aplicacionVersion.CodigoAplicacion, aplicacionVersion.CodigoBuild)

        'Se a aplicação for Genesis, atualiza a mesma.
        If aplicacionVersion.CodigoAplicacion.Trim.ToUpper = Constantes.APP_COD_GENESIS.Trim.ToUpper Then
            ExecutarAtualizacaoGenesis(aplicacionVersion)
        ElseIf forzarSectorDefecto IsNot Nothing Then
            InicializarModulo2(aplicacionVersion, nomeDllAplicacao, formClass, permisoSupervisor, telaLogin)
        Else
            InicializarModulo(aplicacionVersion, nomeDllAplicacao, formClass, permisoSupervisor, telaLogin)
        End If

    End Sub

    Public Shared Function RetornaControlerAplicacion(Of T As IControler)() As T

        Dim caminhoDll As String = String.Empty
        Dim nomeDllAplicacao As String = String.Empty
        Dim formClass As String = String.Empty
        Dim permisoSupervisor As String = String.Empty
        Dim codigoAplicacion As String = String.Empty
        Dim interfaceType As Type = GetType(T)

        Select Case interfaceType
            Case GetType(IControlerATM)
                nomeDllAplicacao = Constantes.ATM_DLL_APP
                formClass = Constantes.ATM_FRM_CLASS
                permisoSupervisor = Constantes.ATM_PER_SUPERVISOR
                codigoAplicacion = Constantes.APP_COD_ATM
                'Case GetType(IControlerConteo)
                '    nomeDllAplicacao = Constantes.CONTEO_DLL_APP
                '    formClass = Constantes.CONTEO_FRM_CLASS
                '    permisoSupervisor = Constantes.CONTEO_PER_SUPERVISOR
                'Case GetType(IControlerEmulador)
                '    nomeDllAplicacao = Constantes.EMULADOR_DLL_APP
                '    formClass = Constantes.EMULADOR_FRM_CLASS
                '    permisoSupervisor = Constantes.EMULADOR_PER_SUPERVISOR
                'Case GetType(IControlerSalidas)
                '    nomeDllAplicacao = Constantes.SALIDAS_DLL_APP
                '    formClass = Constantes.SALIDAS_FRM_CLASS
                '    permisoSupervisor = Constantes.SALIDAS_PER_SUPERVISOR
                'Case GetType(IControlerConteo)
                '    nomeDllAplicacao = Constantes.SUPERVISOR_CONTEO_DLL_APP
                '    formClass = Constantes.SUPERVISOR_CONTEO_FRM_CLASS
                '    permisoSupervisor = Constantes.SUPERVISOR_PER_SUPERVISOR
                'Case GetType(IControlerRecepcionYEnvio)
                '    nomeDllAplicacao = Constantes.RECEPCIONYENVIO_DLL_APP
                '    formClass = Constantes.RECEPCIONYENVIO_FRM_CLASS
                '    permisoSupervisor = Constantes.RECEPCIONYENVIO_PER_SUPERVISOR
                'Case GetType(IControlerGenesis)
                '    nomeDllAplicacao = String.Empty
                '    formClass = String.Empty
                '    permisoSupervisor = String.Empty
            Case GetType(IControlerGESalidas)
                nomeDllAplicacao = Constantes.GE_SALIDAS_DLL_APP
                formClass = Constantes.GE_SALIDAS_FRM_CLASS
                permisoSupervisor = Constantes.GE_SALIDAS_PER_SUPERVISOR
                codigoAplicacion = Constantes.APP_COD_GE_SALIDAS
        End Select


        ' Recupera a aplicação corrente
        Dim aplicacionVersion As ContractoServicio.Login.EjecutarLogin.AplicacionVersion = (From ap In Parametros.Parametros.Aplicaciones _
                                                                                    Where ap.CodigoAplicacion = codigoAplicacion).FirstOrDefault

#If DEBUG Then

        aplicacionVersion.CodigoBuild = "DEBUG"
        aplicacionVersion.DesURLServicio = Parametros.Parametros.URLServicio

#End If

        ' Recupera o Diretório de onde os arquivos foram baixados e extraídos
        _DiretorioArquivosBaixado = String.Format("{0}\{1}\{2}\", _DiretorioCorrente, aplicacionVersion.CodigoAplicacion, aplicacionVersion.CodigoBuild)

        'Se a aplicação for Genesis, atualiza a mesma.
        If aplicacionVersion.CodigoAplicacion.Trim.ToUpper = Constantes.APP_COD_GENESIS.Trim.ToUpper Then
            ExecutarAtualizacaoGenesis(aplicacionVersion)

            Return Nothing
        Else
            'InicializarModulo(aplicacionVersion, nomeDllAplicacao, formClass, permisoSupervisor, telaLogin, [Enum].GetName(GetType(ATMForm), form), parametrosModulo)

            Return CarregarModulo(Of T)(aplicacionVersion, nomeDllAplicacao, formClass)
        End If

    End Function

    ''' <summary>
    ''' Atualiza a aplicação genesis
    ''' </summary>
    ''' <param name="aplicacionVersion"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [fmatosinhos] 12/07/2012 Criado
    ''' </history>
    Private Shared Sub ExecutarAtualizacaoGenesis(aplicacionVersion As ContractoServicio.Login.EjecutarLogin.AplicacionVersion)

        Dim caminhoArquivo As String = String.Empty

        ' Recupera o caminho onde está o arquivo EXE que está em execução 
        caminhoArquivo = RetornarCaminhoCompletoArquivo(_DiretorioCorrente, Constantes.GENESIS_EXE_APP, "exe")

        ' Verifica se a exe principal existe
        If Not (VerificarVersionArchivo(aplicacionVersion, caminhoArquivo)) Then

            'Remove o diretório onde será baixado a versão da aplicação
            RemoverDiretorio(Path.Combine(_DiretorioCorrente, aplicacionVersion.CodigoAplicacion))

            ' Recupera o caminho onde foi baixado o arquivo EXE 
            caminhoArquivo = RetornarCaminhoCompletoArquivo(_DiretorioArquivosBaixado, Constantes.GENESIS_EXE_APP, "exe")

            ' Extrai a versão do modulo
            ExtrairVersao(aplicacionVersion, caminhoArquivo)

            Dim caminhoActualizador As String = RetornarCaminhoCompletoArquivo(_DiretorioArquivosBaixado, Constantes.ACTUALIZADOR_EXE_APP, "exe")

            'Utiliza o caracter pipe (|) para separar os parametros, pois houve problema ao utilizar o arguments args pela aspas duplas e espaços
            Dim strArgs As String = _DiretorioArquivosBaixado.Trim & "|" & _
                                    _DiretorioCorrente.Trim & "|" & _
                                    Constantes.GENESIS_EXE_APP.Trim

            AtualizouGenesis = True

            'Chama o atualizador, passando os seguintes Parâmetros:
            '0 - Caminho de origem dos arquivos (onde os arquivos foram extraídos)
            '1 - Caminho de destino de onde os arquivos serão copiados
            '2 - Nome do processo da aplicação (sem a extensão) a ser encerrada e após a atualização ser reaberto.
            Process.Start(caminhoActualizador, strArgs)

        End If

    End Sub

    ''' <summary>
    ''' Retorna o caminho completo do arquivo
    ''' </summary>
    ''' <param name="caminho">Caminho onde se encontra o arquivo</param>
    ''' <param name="nomeArquivo">Nome do arquivo (sem a extensão)</param>
    ''' <param name="extensaoArquivo">Extensão do arquivo</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [fmatosinhos] 13/07/2012 Criado
    ''' </history>
    Private Shared Function RetornarCaminhoCompletoArquivo(caminho As String, _
                                                           nomeArquivo As String, _
                                                           extensaoArquivo As String) As String

        Return String.Format("{0}.{1}", Path.Combine(caminho, nomeArquivo), extensaoArquivo)

    End Function

    ''' <summary>
    ''' Remove o diretório, subdiretório e arquivos
    ''' </summary>
    ''' <param name="diretorio">Diretório que será removido</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [fmatosinhos] 12/07/2012 Criado
    ''' </history>
    Private Shared Sub RemoverDiretorio(diretorio As String)
        Try

            'Caso o diretório exista, exclua o mesmo e todos os seus subdiretórios
            If Directory.Exists(diretorio) Then
                Directory.Delete(diretorio, True)
            End If

        Catch ex As UnauthorizedAccessException
            Throw New UnauthorizedAccessException(String.Format(Traduzir("atu_msg_usuarionaotempermissaomanipulardiretorio"), diretorio))
        End Try

    End Sub


    ''' <summary>
    ''' Método que efetua as configurações necessária para executar uma versão específica de um módulo
    ''' </summary>
    ''' <param name="aplicacionVersion"></param>
    ''' <param name="nomeDllAplicacao"></param>
    ''' <param name="startupClass"></param>
    ''' <param name="permisoSupervisor"></param>
    ''' <param name="telaLogin"></param>
    ''' <param name="startupFormName"></param>
    ''' <remarks></remarks>
    Private Shared Sub InicializarModulo(aplicacionVersion As ContractoServicio.Login.EjecutarLogin.AplicacionVersion,
                                         nomeDllAplicacao As String,
                                         startupClass As String,
                                         permisoSupervisor As String,
                                         Optional telaLogin As Boolean = False,
                                         Optional startupFormName As String = "",
                                         Optional parametrosModulo As IParametroModulo = Nothing)

        ' Recupera o caminho da DLL
        Dim caminhoArquivo As String = String.Empty

        caminhoArquivo = String.Format("{0}{1}.dll", _DiretorioArquivosBaixado, nomeDllAplicacao)

        ' Extrai a versão do modulo
        ExtrairVersao(aplicacionVersion, caminhoArquivo)

        ' se o usuário atribuído só modulo ou não é tela de Login
        If Not telaLogin OrElse (Parametros.Parametros.Aplicaciones.FindAll(Function(a) a.CodigoAplicacion <> Constantes.APP_COD_GENESIS).Count = 1 AndAlso _
                                 Parametros.Parametros.Aplicaciones.FindAll(Function(a) a.CodigoAplicacion = Constantes.APP_COD_GE_SALIDAS).Count = 0) Then

            ' Verifica se a Dll principal existe
            If (VerificarVersionArchivo(aplicacionVersion, caminhoArquivo)) Then
                ' Carrega as permissões do módulo
                CarregarPermisosModulo(aplicacionVersion.CodigoAplicacion)

                ' Adiciona a coleção de aplicações abertas
                AplicacionesAbertas.Add(aplicacionVersion)

                ' Recupera o caminho do arquivo de configuração
                'Dim caminhoConfig As String = String.Format("{0}\{1}.exe.config", _DiretorioCorrente, My.Application.Info.ProductName)
                Dim caminhoConfig As String = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile

                'Define as configurações do novo domínio
                Dim ads As New AppDomainSetup()
                ads.ApplicationBase = _DiretorioArquivosBaixado
                ads.DisallowBindingRedirects = False
                ads.DisallowCodeDownload = True
                ads.ConfigurationFile = caminhoConfig

                ' Verifica se o dominio corrente está ativo
                If AppDomain.CurrentDomain IsNot Nothing AndAlso AppDomain.CurrentDomain.ApplicationTrust IsNot Nothing Then
                    ads.ApplicationTrust = AppDomain.CurrentDomain.ApplicationTrust
                End If

                ' Cria uma nova aplicação para o módulo selecionado
                Dim aplicacionDomain As AppDomain = AppDomain.CreateDomain(aplicacionVersion.CodigoAplicacion, Nothing, ads)

                ' Executa a aplicação criada
                Dim objAplicacion As Prosegur.Genesis.Utilidad.IControler = aplicacionDomain.CreateInstanceAndUnwrap(nomeDllAplicacao, startupClass)

                Dim gerenciador As New GerenciadorAplicacion()

                'ExecutarModulo
                Dim threadMod As New Thread(New ParameterizedThreadStart(AddressOf ExecutarModulo))
                threadMod.IsBackground = True
                threadMod.SetApartmentState(ApartmentState.STA)
                threadMod.Start(New ParametroThread() With {
                                .Aplicaciones = Parametros.Parametros.Aplicaciones,
                                .AplicacionVersion = aplicacionVersion,
                                .Aplicacion = objAplicacion,
                                .ModAppDomain = aplicacionDomain,
                                .FormStartup = startupFormName,
                                .Usuario = Parametros.Parametros.InformacionUsuario,
                                .UrlServicio = Parametros.Parametros.URLServicio,
                                .ParametrosModulo = parametrosModulo
                                })

                Thread.Sleep(5000)

            End If

        End If

    End Sub


    ''' <summary>
    ''' Carrega as permissões do modulo selecionado
    ''' </summary>
    ''' <param name="codigoAplicacion">String</param>
    ''' <remarks></remarks>
    Private Shared Sub CarregarPermisosModulo(codigoAplicacion As String)
        If (DirectCast(Parametros.Parametros.InformacionUsuario.Continentes.First.Paises.First _
                .Delegaciones.First, ContractoServicio.Login.EjecutarLogin.DelegacionPlanta).Plantas.First _
                .TiposSectores.Any(Function(t) t.Permisos.Any(Function(p) p.CodigoAplicacion = codigoAplicacion))) Then
            Return
        End If

        Dim app = (From ap In Parametros.Parametros.Aplicaciones _
                                            Where ap.CodigoAplicacion = codigoAplicacion).FirstOrDefault

        Dim peticion As New ContractoLogin.ObtenerPermisos.Peticion
        peticion.codigoDelegacion = Parametros.Parametros.InformacionUsuario.CodigoDelegacion
        peticion.codigoPlanta = Parametros.Parametros.InformacionUsuario.CodigoPlanta
        peticion.codigoPais = Parametros.Parametros.InformacionUsuario.CodigoPais
        peticion.identificadorUsuario = Parametros.Parametros.InformacionUsuario.Identificador
        peticion.identificadorAplicacion = app.OidAplicacion

        'Se necessário
        If (Parametros.Parametros.AgenteComunicacion Is Nothing) Then
            'Inicializa o agente de comunicações.
            Parametros.Parametros.AgenteComunicacion = Comunicacion.Agente.Instancia
        End If

        Dim respuesta As ContractoLogin.ObtenerPermisos.Respuesta = _
            Parametros.Parametros.AgenteComunicacion.RecibirMensaje(Comunicacion.Metodo.ObtenerPermisos2, peticion)

        If Not Util.TratarRetornoServico(respuesta) Then Return

        For Each tSector In DirectCast(respuesta.Delegaciones.First, ContractoServicio.Login.EjecutarLogin.DelegacionPlanta) _
            .Plantas.First.TiposSectores

            Dim tSectorParam = DirectCast(Parametros.Parametros.InformacionUsuario.Continentes.First.Paises.First _
                .Delegaciones.First, ContractoServicio.Login.EjecutarLogin.DelegacionPlanta).Plantas.First _
                .TiposSectores _
                .Where(Function(t) t.Identificador = tSector.Identificador) _
                .FirstOrDefault()

            If tSectorParam IsNot Nothing Then
                For Each tPermiso In tSector.Permisos
                    If Not tSectorParam.Permisos.Contains(tPermiso) Then
                        tSectorParam.Permisos.Add(tPermiso)
                    End If
                Next
            End If
        Next

    End Sub

    Private Shared Sub InicializarModulo2(aplicacionVersion As ContractoServicio.Login.EjecutarLogin.AplicacionVersion,
                                        nomeDllAplicacao As String,
                                        startupClass As String,
                                        permisoSupervisor As String,
                                        Optional telaLogin As Boolean = False,
                                        Optional startupFormName As String = "",
                                        Optional parametrosModulo As IParametroModulo = Nothing)

        ' Recupera o caminho da DLL
        Dim caminhoArquivo As String = String.Empty

        caminhoArquivo = String.Format("{0}{1}.dll", _DiretorioArquivosBaixado, nomeDllAplicacao)

        ' Extrai a versão do modulo
        ExtrairVersao(aplicacionVersion, caminhoArquivo)

        ' se o usuário atribuído só modulo ou não é tela de Login
        If Not telaLogin OrElse (Parametros.Parametros.Aplicaciones.FindAll(Function(a) a.CodigoAplicacion <> Constantes.APP_COD_GENESIS).Count = 1 AndAlso _
                                 Parametros.Parametros.Aplicaciones.FindAll(Function(a) a.CodigoAplicacion = Constantes.APP_COD_GE_SALIDAS).Count = 0) Then

            ' Verifica se a Dll principal existe
            If (VerificarVersionArchivo(aplicacionVersion, caminhoArquivo)) Then

                ' Adiciona a coleção de aplicações abertas
                AplicacionesAbertas.Add(aplicacionVersion)

                ' Recupera o caminho do arquivo de configuração
                'Dim caminhoConfig As String = String.Format("{0}\{1}.exe.config", _DiretorioCorrente, My.Application.Info.ProductName)
                Dim caminhoConfig As String = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile

                'Define as configurações do novo domínio
                Dim ads As New AppDomainSetup()
                ads.ApplicationBase = _DiretorioArquivosBaixado
                ads.DisallowBindingRedirects = False
                ads.DisallowCodeDownload = True
                ads.ConfigurationFile = caminhoConfig

                ' Verifica se o dominio corrente está ativo
                If AppDomain.CurrentDomain IsNot Nothing AndAlso AppDomain.CurrentDomain.ApplicationTrust IsNot Nothing Then
                    ads.ApplicationTrust = AppDomain.CurrentDomain.ApplicationTrust
                End If

                ' Cria uma nova aplicação para o módulo selecionado
                Dim aplicacionDomain As AppDomain = AppDomain.CreateDomain(aplicacionVersion.CodigoAplicacion, Nothing, ads)

                ' Executa a aplicação criada
                Dim objAplicacion As Prosegur.Genesis.Utilidad.IControler = aplicacionDomain.CreateInstanceAndUnwrap(nomeDllAplicacao, startupClass)

                Dim gerenciador As New GerenciadorAplicacion()

                'ExecutarModulo
                ExecutarModulo2(New ParametroThread() With {
                                .Aplicaciones = Parametros.Parametros.Aplicaciones,
                                .AplicacionVersion = aplicacionVersion,
                                .Aplicacion = objAplicacion,
                                .ModAppDomain = aplicacionDomain,
                                .FormStartup = startupFormName,
                                .Usuario = Parametros.Parametros.InformacionUsuario,
                                .UrlServicio = Parametros.Parametros.URLServicio,
                                .ParametrosModulo = parametrosModulo
                                })

            End If

        End If

    End Sub

    ''' <summary>
    ''' Método que extrai a versão do modulo que está banco para a raiz do Genesis
    ''' </summary>
    ''' <param name="aplicacionVersion"></param>
    ''' <param name="caminhoDll"></param>
    ''' <remarks></remarks>
    Private Shared Sub ExtrairVersao(aplicacionVersion As ContractoServicio.Login.EjecutarLogin.AplicacionVersion, caminhoDll As String)

        If Not File.Exists(caminhoDll) AndAlso String.IsNullOrEmpty(aplicacionVersion.DesURLSitio) Then

            Dim mStream As New MemoryStream()
            Dim diretorioDestino As New DirectoryInfo(_DiretorioCorrente & "\" & aplicacionVersion.CodigoAplicacion & "\" & aplicacionVersion.CodigoBuild)
            If (diretorioDestino.Exists) Then
                diretorioDestino.Delete(True)
            End If

            diretorioDestino.Create()

            If Not CopiarDaRedeLocal(aplicacionVersion, diretorioDestino) Then

                CopiarDoBancoDados(aplicacionVersion, diretorioDestino)

            End If

        End If


    End Sub

    ''' <summary>
    ''' Método que verifica a versão do arquivo (exe ou dll)
    ''' </summary>
    ''' <param name="aplicacionVersion"></param>
    ''' <param name="caminhoArquivo"></param>
    Private Shared Function VerificarVersionArchivo(aplicacionVersion As ContractoServicio.Login.EjecutarLogin.AplicacionVersion, caminhoArquivo As String) As Boolean

        Dim _versionArchivo As String = String.Empty
        Dim _versionDiferente As Boolean = False

#If DEBUG Then
        Return True
#End If

        Dim _fileVersionInfo As FileVersionInfo = FileVersionInfo.GetVersionInfo(caminhoArquivo)
        _versionArchivo = _fileVersionInfo.FileVersion

        If _fileVersionInfo.FileMajorPart >= 6 Then
            Dim versionParte As String() = aplicacionVersion.CodigoBuild.Split(".")
            If Integer.Parse(versionParte(0)) <> _fileVersionInfo.FileMajorPart OrElse Integer.Parse(versionParte(1)) <> _fileVersionInfo.FileMinorPart Then
                _versionDiferente = True
            End If
        Else
            If Not aplicacionVersion.CodigoBuild.Equals(_versionArchivo) Then
                _versionDiferente = True
            End If
        End If

        If _versionDiferente Then

            If Not aplicacionVersion.CodigoAplicacion.Equals(Constantes.APP_COD_GENESIS) Then

                'MessageBox.Show(String.Format(Traduzir("000_mgs_versao_distinta"), aplicacionVersion.CodigoBuild, _versionArchivo), Traduzir("000_titulo_msgbox"), Forms.MessageBoxButtons.OK, Forms.MessageBoxIcon.Error)
                Throw New Excepcion.NegocioExcepcion(String.Format(Traduzir("000_mgs_versao_distinta"), aplicacionVersion.CodigoBuild, _versionArchivo))

            End If

            Return False

        End If

        Return True

    End Function

    ''' <summary>
    ''' Executa o modulo aberto
    ''' </summary>
    ''' <param name="data">ParametroThread</param>
    ''' <remarks></remarks>
    Private Shared Sub ExecutarModulo(data As Object)

        Dim parametro As ParametroThread = DirectCast(data, ParametroThread)

        Try
            parametro.Aplicacion.Aplicaciones = parametro.Aplicaciones
            parametro.Aplicacion.Usuario = parametro.Usuario
            parametro.Aplicacion.InformacionUsuarioCompleta = ClonarObjeto(Parametros.Parametros.InformacionUsuario)

            ' Loga no modulo passando os dados do usuario como parâmetro
            forzarAbrirModulo = parametro.Aplicacion.LogarUsuario(RecuperarPermisosUsuario(parametro.AplicacionVersion.CodigoAplicacion, parametro.AplicacionVersion.CodigoVersion), parametro.AplicacionVersion.DesURLServicio)

        Catch Ex As Excepcion.NegocioExcepcion

            ' Exibe uma mensagem com a descrição do erro
            'WindowBase.TratarErro(Ex)

            Throw

        Catch ex As Exception

            ' Exibe uma mensagem com a descrição do erro
            'WindowBase.TratarErro(ex)

            Throw

        Finally

            ' Remove o modulo da coleção
            AplicacionesAbertas.Remove(parametro.AplicacionVersion)

            If AplicacionesAbertas.Count = 0 Then
                RaiseEvent TodasAplicacionesCerradas()
            End If

            ' Descarrega o modulo que foi fechado
            AppDomain.Unload(parametro.ModAppDomain)

            ' Força o fechamento da Thread
            Thread.CurrentThread.Abort()

        End Try

    End Sub

    Private Shared Sub ExecutarModulo2(data As Object)

        Dim parametro As ParametroThread = DirectCast(data, ParametroThread)

        Try
            parametro.Aplicacion.Aplicaciones = parametro.Aplicaciones
            parametro.Aplicacion.Usuario = parametro.Usuario
            parametro.Aplicacion.InformacionUsuarioCompleta = ClonarObjeto(Parametros.Parametros.InformacionUsuario)

            ' Loga no modulo passando os dados do usuario como parâmetro
            forzarAbrirModulo = parametro.Aplicacion.LogarUsuario(RecuperarPermisosUsuario(parametro.AplicacionVersion.CodigoAplicacion, parametro.AplicacionVersion.CodigoVersion), parametro.AplicacionVersion.DesURLServicio)


        Catch Ex As Excepcion.NegocioExcepcion

            ' Exibe uma mensagem com a descrição do erro
            'WindowBase.TratarErro(Ex)

            Throw

        Catch ex As Exception

            ' Exibe uma mensagem com a descrição do erro
            'WindowBase.TratarErro(ex)

            Throw

        Finally

            ' Remove o modulo da coleção
            AplicacionesAbertas.Remove(parametro.AplicacionVersion)

            If AplicacionesAbertas.Count = 0 Then
                RaiseEvent TodasAplicacionesCerradas()
            End If

            ' Descarrega o modulo que foi fechado
            AppDomain.Unload(parametro.ModAppDomain)

        End Try

    End Sub

    ''' <summary>
    ''' Recupera as permisões do usuario de acordo com o código da aplicação
    ''' </summary>
    ''' <param name="codigoAplicacion">String</param>
    ''' <returns>Genesis.ContractoServicio.Login.EjecutarLogin.Usuario</returns>
    ''' <remarks></remarks>
    Private Shared Function RecuperarPermisosUsuario(codigoAplicacion As String, CodVersion As String) As Genesis.ContractoServicio.Login.EjecutarLogin.Usuario

        ' Recupera as informações do usuario
        Dim objUsuario As Genesis.ContractoServicio.Login.EjecutarLogin.Usuario = ClonarObjeto(Parametros.Parametros.InformacionUsuario)

        ' Verifica se o usuário tem permissão em pelo menos uma delegação
        If objUsuario.Continentes IsNot Nothing AndAlso objUsuario.Continentes.Count > 0 AndAlso _
           objUsuario.Continentes.First.Paises IsNot Nothing AndAlso objUsuario.Continentes.First.Paises.Count > 0 AndAlso _
           objUsuario.Continentes.First.Paises.First.Delegaciones IsNot Nothing AndAlso objUsuario.Continentes.First.Paises.First.Delegaciones.Count > 0 Then

            Dim objDelegacionConverter As List(Of Genesis.ContractoServicio.Login.EjecutarLogin.Delegacion) = ClonarObjeto(objUsuario.Continentes.First.Paises.First.Delegaciones)

            Dim NumVersion As Integer = Convert.ToInt32(CodVersion.Replace(".", ""))

            Dim TrabalhaPorPlanta As Boolean = (NumVersion >= ContractoServicio.Constantes.VERSION_INICIO_TRABALHAR_POR_PLANTA)

            ' Para cada delegação existente
            For Each delegacion In If(TrabalhaPorPlanta, objUsuario.Continentes.First.Paises.First.Delegaciones, objDelegacionConverter)

                Dim objDelegacion As Genesis.ContractoServicio.Login.EjecutarLogin.DelegacionPlanta =
                    DirectCast(delegacion, Genesis.ContractoServicio.Login.EjecutarLogin.DelegacionPlanta)

                If TrabalhaPorPlanta Then

                    If objDelegacion.Plantas IsNot Nothing AndAlso objDelegacion.Plantas.Count > 0 Then

                        For Each Planta In objDelegacion.Plantas

                            ' Verifica se existe setores
                            If Planta.TiposSectores IsNot Nothing AndAlso Planta.TiposSectores.Count > 0 Then

                                ' Verifica se existe algum setor logado
                                If objUsuario.SectorLogado IsNot Nothing AndAlso objUsuario.SectorLogado.TipoSector IsNot Nothing Then
                                    ' Remove os tipos de setores que não são do tipo do setor logado
                                    Planta.TiposSectores.RemoveAll(Function(r) r.Codigo <> objUsuario.SectorLogado.TipoSector.Codigo)
                                End If

                                ' Para cada tipo de setor existente
                                For Each tiposector In Planta.TiposSectores

                                    ' Recupera somente as permissões da aplicação acessada
                                    tiposector.Permisos.RemoveAll(Function(f) f.CodigoAplicacion <> codigoAplicacion)

                                Next

                                ' Remove todos os setores sem permissões
                                Planta.TiposSectores.RemoveAll(Function(f) f.Permisos.Count = 0)

                            End If

                        Next

                    End If

                Else

                    objUsuario.Continentes.First.Paises.First.Delegaciones = New List(Of Genesis.ContractoServicio.Login.EjecutarLogin.Delegacion)

                    If delegacion.Sectores IsNot Nothing AndAlso delegacion.Sectores.Count > 0 Then

                        For Each Sector In delegacion.Sectores

                            ' Recupera somente as permissões da aplicação acessada
                            Sector.Permisos.RemoveAll(Function(f) f.CodigoAplicacion <> codigoAplicacion)

                        Next

                        ' Remove todos os setores sem permissões
                        delegacion.Sectores.RemoveAll(Function(f) f.Permisos.Count = 0)

                    End If

                    objUsuario.Continentes.First.Paises.First.Delegaciones.Add(New Genesis.ContractoServicio.Login.EjecutarLogin.Delegacion With { _
                                                                              .GMT = delegacion.GMT, _
                                                                              .CantidadMetrosBase = delegacion.CantidadMetrosBase, _
                                                                              .CantidadMinutosIni = delegacion.CantidadMinutosIni, _
                                                                              .CantidadMinutosSalida = delegacion.CantidadMinutosSalida, _
                                                                              .Codigo = delegacion.Codigo, _
                                                                              .DelegacionesLegado = delegacion.DelegacionesLegado, _
                                                                              .Identificador = delegacion.Identificador, _
                                                                              .Nombre = delegacion.Nombre, _
                                                                              .Sectores = delegacion.Sectores, _
                                                                              .VeranoAjuste = delegacion.VeranoAjuste, _
                                                                              .VeranoFechaHoraFin = delegacion.VeranoFechaHoraFin, _
                                                                              .VeranoFechaHoraIni = delegacion.VeranoFechaHoraIni, _
                                                                              .Zona = delegacion.Zona})

                End If

            Next

            ' caso não tenha permissão
        ElseIf objUsuario.Continentes.Count = 0 OrElse _
            objUsuario.Continentes.First.Paises.Count = 0 OrElse _
            objUsuario.Continentes.First.Paises.First.Delegaciones.Count = 0 OrElse _
            DirectCast(objUsuario.Continentes.First.Paises.First.Delegaciones.First, Genesis.ContractoServicio.Login.EjecutarLogin.DelegacionPlanta).Plantas.Count = 0 OrElse _
            DirectCast(objUsuario.Continentes.First.Paises.First.Delegaciones.First, Genesis.ContractoServicio.Login.EjecutarLogin.DelegacionPlanta).Plantas.First.TiposSectores.Count = 0 OrElse _
            DirectCast(objUsuario.Continentes.First.Paises.First.Delegaciones.First, Genesis.ContractoServicio.Login.EjecutarLogin.DelegacionPlanta).Plantas.First.TiposSectores.First.Permisos.Count = 0 Then

            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_ATENCION, Traduzir("mdl_msg_sempermissao"))

        End If

        ' Retorna o objeto de usuário
        Return objUsuario

    End Function

    ''' <summary>
    ''' Retorna cópia do objeto passado como parâmetro
    ''' </summary>
    ''' <param name="obj"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function ClonarObjeto(obj As Object) As Object

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

    ''' <summary>
    ''' Trata o retorno dos serviços
    ''' </summary>
    ''' <param name="objRespuesta">Objeto com a resposta da requisição</param>
    ''' <param name="ExhibirMensaje">Indica se a mensagem de negócio deve ser exibida na tela</param>
    ''' <returns>retorna true para validação ok e false para erro</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [prezende] 22/05/2012 Criado
    ''' </history>
    Public Shared Function TratarRetornoServico(ByRef objRespuesta As ContractoServicio.RespuestaGenerico, _
                                                Optional ExhibirMensaje As Boolean = True) As Boolean

        ' verifica se o retorno não é nothing
        If objRespuesta IsNot Nothing Then

            ' se houve erro e o código for maior ou igual a 100
            If objRespuesta.CodigoError >= 100 Then

                If ExhibirMensaje Then
                    ' exibir mensagem
                    'MessageBox.Show(objRespuesta.MensajeError, Traduzir("000_titulo_msgbox"), Forms.MessageBoxButtons.OK, Forms.MessageBoxIcon.Information)
                    Throw New Excepcion.NegocioExcepcion(objRespuesta.CodigoError, objRespuesta.MensajeError)
                End If

            ElseIf objRespuesta.CodigoError > 0 AndAlso objRespuesta.CodigoError < 100 Then

                'loga o erro
                LogarErroAplicacao(objRespuesta)

                If ExhibirMensaje Then
                    ' exibir mensagem
                    'MessageBox.Show(objRespuesta.MensajeError, Traduzir("000_titulo_msgbox"), Forms.MessageBoxButtons.OK, Forms.MessageBoxIcon.Error)
                    Throw New Excepcion.NegocioExcepcion(objRespuesta.CodigoError, objRespuesta.MensajeError)
                End If

            ElseIf objRespuesta.CodigoError = 0 Then
                ' resposta do serviço ok
                Return True
            End If

        End If

        ' se chegou até aqui, houve algum erro no serviço
        Return False

    End Function



#Region "[LOGAR ERRO APLICAÇÃO]"

    ''' <summary>
    ''' Metodo genérico para logar os erros retornados pelos webservices.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [prezende] 22/05/2012 Criado
    ''' </history>
    Public Shared Sub LogarErroAplicacao(Respuesta As ContractoServicio.RespuestaGenerico)
        'Grava log de Erro
        LogarErroAplicacao(Respuesta, String.Empty)

    End Sub

    ''' <summary>
    ''' Metodo genérico para logar os erros retornados pelos webservices.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [prezende] 22/05/2012 Criado
    ''' </history>
    Public Shared Sub LogarErroAplicacao(Respuesta As ContractoServicio.RespuestaGenerico, OutrasInformacoes As String)

        'Cria objetos Peticion e Respuesta de ContractoServicio.LogarErro
        Dim Peticion As New ContractoServicio.LogarErro.Peticion()
        Dim RespuestaLog As ContractoServicio.LogarErro.Respuesta

        'Preenche parâmetros de entrada para logar erro
        Peticion.FechaError = DateTime.Now

        ' Verifica se a informação do usuário foi preenchida
        If Parametros.Parametros.InformacionUsuario IsNot Nothing Then
            Peticion.CodigoUsuario = Parametros.Parametros.InformacionUsuario.Login
            Peticion.CodigoDelegacion = Parametros.Parametros.InformacionUsuario.CodigoDelegacion
        End If

        Peticion.DescripcionError = String.Format("{0} - {1}", Respuesta.CodigoError, Respuesta.MensajeError)
        Peticion.DescripcionOtro = OutrasInformacoes

        Try

            'Chama o serviço responsável pela gravação do log
            RespuestaLog = Parametros.Parametros.AgenteComunicacion.RecibirMensaje(Comunicacion.Metodo.LogarErro, Peticion)

            If RespuestaLog.CodigoError <> 0 Then

                'Grava a exceção localmente caso ocorra algum problema.
                SerializaErroAplicacao(Peticion)

            End If

        Catch ex As Exception

            'Grava a exceção localmente caso ocorra algum problema.
            SerializaErroAplicacao(Peticion)

        End Try

    End Sub

    ''' <summary>
    ''' Método responsável por gravar, em formato XML, o erro no diretório pre-definido
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <remarks></remarks>
    Public Shared Sub SerializaErroAplicacao(Peticion As ContractoServicio.LogarErro.Peticion)

        Try

            'Verifica se o diretório padrão de erro da aplicação está criado
            If Not Directory.Exists(Parametros.Parametros.PathXmlError) Then

                'Cria diretório automaticamente
                Directory.CreateDirectory(Parametros.Parametros.PathXmlError)

            End If

            'Serializa o objeto Peticio contendo as informações da exceção
            Dim serializer As New XmlSerializer(GetType(ContractoServicio.LogarErro.Peticion))
            Using writer As New StreamWriter(Parametros.Parametros.PathXmlError & System.Guid.NewGuid().ToString() & ".xml")
                serializer.Serialize(writer, Peticion)
                writer.Close()
            End Using

        Catch ex As Exception

            'Caso ocorra algum problema ao gravar o arquivo xml de erro localmente
            'Grava a exeção no EventViewer do Windows
            LogarErroEventViewer(Peticion)

        End Try

    End Sub

    ''' <summary>
    ''' Responsável por gravar a exceção no EventViewer.
    ''' </summary>
    Private Shared Sub LogarErroEventViewer(Peticion As ContractoServicio.LogarErro.Peticion)
        Try

            'Verifica se ja existe no Event Viewer uma entrada para armazenar as informações.
            If Not Diagnostics.EventLog.Exists(ContractoServicio.Constantes.NOME_LOG_EVENTOS) Then

                'Caso não exista cria uma.
                Diagnostics.EventLog.CreateEventSource(ContractoServicio.Constantes.NOME_LOG_EVENTOS, ContractoServicio.Constantes.NOME_LOG_EVENTOS)

            End If

            'Instancia objeto StringBuilder para configurar a mensagem a ser gravada
            Dim Mensagem As New StringBuilder()

            Mensagem.Append(Traduzir("000_LogarErroMensagemEventViewer"))
            Mensagem.Append(Environment.NewLine)
            Mensagem.Append("------------------------------------------------------------------------------------")
            Mensagem.Append(Environment.NewLine)
            Mensagem.Append("Exception: ")
            Mensagem.Append(Peticion.DescripcionError)
            Mensagem.Append(Environment.NewLine)
            Mensagem.Append("------------------------------------------------------------------------------------")

            Diagnostics.EventLog.WriteEntry(ContractoServicio.Constantes.NOME_LOG_EVENTOS, Mensagem.ToString(), EventLogEntryType.Error)

        Catch ex As Exception
            'Tenta gravar no registro
        End Try
    End Sub

#End Region

    Private Shared Function CarregarModulo(Of T As IControler)(aplicacionVersion As ContractoServicio.Login.EjecutarLogin.AplicacionVersion,
                                           nomeDllAplicacao As String,
                                           formClass As String,
                                           Optional telaLogin As Boolean = False) As T
        ' Recupera o caminho da DLL
        Dim caminhoArquivo As String = String.Empty

        caminhoArquivo = String.Format("{0}{1}.dll", _DiretorioArquivosBaixado, nomeDllAplicacao)

        ' Extrai a versão do modulo
        ExtrairVersao(aplicacionVersion, caminhoArquivo)

        ' se o usuário atribuído só modulo ou não é tela de Login
        If Not telaLogin OrElse (Parametros.Parametros.Aplicaciones.FindAll(Function(a) a.CodigoAplicacion <> Constantes.APP_COD_GENESIS).Count = 1 AndAlso _
                                 Parametros.Parametros.Aplicaciones.FindAll(Function(a) a.CodigoAplicacion = Constantes.APP_COD_GE_SALIDAS).Count = 0) Then

            ' Verifica se a Dll principal existe
            If (VerificarVersionArchivo(aplicacionVersion, caminhoArquivo)) Then

                ' Adiciona a coleção de aplicações abertas
                AplicacionesAbertas.Add(aplicacionVersion)

                Dim currentDomain As AppDomain = AppDomain.CurrentDomain
                AddHandler currentDomain.AssemblyResolve, AddressOf MyResolveEventHandler

                Dim modulo As Assembly = Assembly.LoadFrom(caminhoArquivo)
                Dim tipoControler As Type = modulo.GetType(formClass)
                Dim controler As T = Activator.CreateInstance(tipoControler)

                If controler.Aplicaciones Is Nothing Then
                    controler.Aplicaciones = New ContractoServicio.Login.EjecutarLogin.AplicacionVersionColeccion()
                End If

                controler.Aplicaciones.Add(aplicacionVersion)
                Parametros.Parametros.InformacionUsuario = ClonarObjeto(Parametros.Parametros.InformacionUsuarioCompleta)

                ' Carrega as permissões do módulo
                CarregarPermisosModulo(aplicacionVersion.CodigoAplicacion)

                controler.Usuario = RecuperarPermisosUsuario(aplicacionVersion.CodigoAplicacion, aplicacionVersion.CodigoVersion)
                controler.UrlServicio = Parametros.Parametros.URLServicio

                Return controler

            End If

        End If

        Return Nothing
    End Function

    Private Shared Function MyResolveEventHandler(sender As Object, args As ResolveEventArgs) As Assembly
        Dim folderPath As String = _DiretorioArquivosBaixado
        Dim assemblyPath As String = Path.Combine(folderPath, New AssemblyName(args.Name).Name + ".dll")
        If File.Exists(assemblyPath) = False Then
            Return Nothing
        End If
        Dim assembly As Assembly = assembly.LoadFrom(assemblyPath)
        Return assembly
    End Function

    Private Shared Sub Unzip(stream As MemoryStream, diretorioDestino As DirectoryInfo)
        'Retorna stream para a posição 0
        stream.Seek(0, SeekOrigin.Begin)

        Using zip As ZipFile = ZipFile.Read(stream)
            Dim e As ZipEntry
            For Each e In zip
                e.Extract(diretorioDestino.FullName, ExtractExistingFileAction.OverwriteSilently)
            Next
        End Using
    End Sub

    Private Shared Sub Unzip(archivo As String, diretorioDestino As DirectoryInfo)

        Using zip As ZipFile = ZipFile.Read(archivo)
            Dim e As ZipEntry
            For Each e In zip
                e.Extract(diretorioDestino.FullName, ExtractExistingFileAction.OverwriteSilently)
            Next
        End Using
    End Sub

    Private Shared Function CopiarDaRedeLocal(aplicacionVersion As ContractoServicio.Login.EjecutarLogin.AplicacionVersion, diretorioDestino As DirectoryInfo) As Boolean
        If Parametros.Parametros.InformacionUsuario Is Nothing Then
            Return False
        End If

        Dim proxyIntegracion As New ProxyIacIntegracion()
        Dim peticion As New Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio.GetParametrosDelegacionPais.Peticion
        peticion.CodigoAplicacion = Constantes.APP_COD_GENESIS
        peticion.CodigoDelegacion = Parametros.Parametros.InformacionUsuario.CodigoDelegacion

        Dim respuesta = proxyIntegracion.GetParametrosDelegacionPais(peticion)
        If respuesta.CodigoError <> Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT Then
            Return False
        End If

        Dim parametroIAC = respuesta.Parametros.Find(Function(p) p.CodigoParametro = "DireccionActualizacionGenesis")

        If parametroIAC Is Nothing Then
            Return False
        End If

        ' Busco da rede local
        Dim caminho As String = String.Format(parametroIAC.ValorParametro, aplicacionVersion.CodigoAplicacion, aplicacionVersion.CodigoVersion)
        Dim arquivoOrigem As String = String.Format("{0}\{1}\{2}\file.zip", caminho, aplicacionVersion.CodigoAplicacion, aplicacionVersion.CodigoVersion)
        Dim chechsumArquivoOrigem As String = String.Format("{0}\{1}\{2}\checksum", caminho, aplicacionVersion.CodigoAplicacion, aplicacionVersion.CodigoVersion)

        If File.Exists(arquivoOrigem) Then
            _TentativasAtualizacaoViaRede += 1

            Dim filestreamArquivoOrigem As New FileStream(arquivoOrigem, FileMode.Open, FileAccess.Read)

            Dim streamReader As New StreamReader(chechsumArquivoOrigem)
            Dim hashOrigem As String = streamReader.ReadLine().Trim()

            Dim hash As Byte()
            Using objMD5 As MD5 = MD5.Create
                hash = objMD5.ComputeHash(filestreamArquivoOrigem)
            End Using

            If hashOrigem <> BitConverter.ToString(hash).Replace("-", "").ToLower() Then
                If _TentativasAtualizacaoViaRede < 3 Then
                    ' Efetua uma nova tentativa
                    Return CopiarDaRedeLocal(aplicacionVersion, diretorioDestino)
                Else
                    ' Se já é a terceira tentativa, aborto e faço o download via banco
                    Return False
                End If
            End If

            Unzip(arquivoOrigem, diretorioDestino)

            Return True
        Else
            Return False
        End If
    End Function

    Private Shared Sub CopiarDoBancoDados(aplicacionVersion As ContractoServicio.Login.EjecutarLogin.AplicacionVersion, diretorioDestino As DirectoryInfo)
        Dim objPeticionObtenerAplicacionVersion As New ContractoServicio.Login.ObtenerAplicacionVersion.Peticion()
        objPeticionObtenerAplicacionVersion.CodigoAplicacion = aplicacionVersion.CodigoAplicacion
        objPeticionObtenerAplicacionVersion.CodigoVersion = aplicacionVersion.CodigoVersion

        Dim objRespuestaObtenerAplicacionVersion As ContractoServicio.Login.ObtenerAplicacionVersion.Respuesta
        objRespuestaObtenerAplicacionVersion = Parametros.Parametros.AgenteComunicacion.RecibirMensaje(Comunicacion.Metodo.ObtenerAplicacionVersion, objPeticionObtenerAplicacionVersion)

        ' Valida se ocorreu algum erro na chamada do serviço.
        If Not TratarRetornoServico(objRespuestaObtenerAplicacionVersion) Then Exit Sub

        If (objRespuestaObtenerAplicacionVersion.AplicacionVersion IsNot Nothing _
            AndAlso objRespuestaObtenerAplicacionVersion.AplicacionVersion.ArchivoVersion IsNot Nothing) Then

            Dim stream As New MemoryStream()

            stream.Write(objRespuestaObtenerAplicacionVersion.AplicacionVersion.ArchivoVersion, 0, objRespuestaObtenerAplicacionVersion.AplicacionVersion.ArchivoVersion.Length)

            Unzip(stream, diretorioDestino)

        End If
    End Sub

End Class


Public Class ParametroThread
    Public AplicacionVersion As ContractoServicio.Login.EjecutarLogin.AplicacionVersion
    Public Aplicacion As Prosegur.Genesis.Utilidad.IControler
    Public ModAppDomain As AppDomain
    Public Usuario As Genesis.ContractoServicio.Login.EjecutarLogin.Usuario

    Property Aplicaciones As ContractoServicio.Login.EjecutarLogin.AplicacionVersionColeccion

    Property FormStartup As String

    Property UrlServicio As String

    Property ParametrosModulo As IParametroModulo

    Property GerenciadorAplicacion As GerenciadorAplicacion

End Class