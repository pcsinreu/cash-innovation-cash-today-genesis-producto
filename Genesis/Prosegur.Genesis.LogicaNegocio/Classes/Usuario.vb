Imports Prosegur.Framework.Dicionario.Tradutor
Imports System.Xml.Serialization
Imports System.IO
Imports System.Text
Imports Prosegur.Genesis
Imports Prosegur.Genesis.Comunicacion


Public Class Usuario
    Inherits EntidadeBase

#Region "[ENUMERACOES]"

    ''' <summary>
    ''' Permissões de acesso no AD
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  20/01/2011  criado
    ''' </history>
    Public Enum Permisos
        AsociarTiraCajeroRed
        MantenerFormatoTira
        RegistrarTira
    End Enum

#End Region

#Region "[VARIÁVEIS]"

    Private _login As String
    Private _chave As String
    Private _continentesPermiso As New List(Of ContractoServicio.Login.EjecutarLogin.Continente)
    Private _resultadoOperacionLogin As ContractoServicio.Login.EjecutarLogin.ResultadoOperacionLoginLocal
    Private _aplicacionVersion As ContractoServicio.Login.EjecutarLogin.AplicacionVersion

#End Region

#Region "[PROPRIEDADES]"

    Public Property Login As String
        Get
            Return _login
        End Get
        Set(value As String)
            _login = value
        End Set
    End Property

    Public Property Chave As String
        Get
            Return _chave
        End Get
        Set(value As String)
            _chave = value
        End Set
    End Property

    ''' <summary>
    ''' Permissões do usuário
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  19/01/2011  criado
    ''' </history>
    Public ReadOnly Property ContinentesPermiso As List(Of ContractoServicio.Login.EjecutarLogin.Continente)
        Get
            Return _continentesPermiso
        End Get
    End Property

    ''' <summary>
    ''' Resultado da operação de login
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ResultadoOperacionLogin As ContractoServicio.Login.EjecutarLogin.ResultadoOperacionLoginLocal
        Get
            Return _resultadoOperacionLogin
        End Get
    End Property

    ''' <summary>
    ''' Dados da aplicação e versão que o usuário logou
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property AplicacionVersion As ContractoServicio.Login.EjecutarLogin.AplicacionVersion
        Get
            Return _aplicacionVersion
        End Get
    End Property

#End Region

#Region "[MÉTODOS]"

    ''' <summary>
    ''' Método que verifica se a permiso existe na lista de permissões do usuário
    ''' </summary>
    ''' <param name="permiso">Nome da permissão</param>
    ''' <returns>True - Caso encontre a permiso / False - Caso não encontre a permiso</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  20/01/2011  Criado
    ''' </history>
    Public Function ValidarPermiso(permiso As Permisos) As Boolean

        Dim nomePermiso As String = String.Empty

        Select Case permiso
            Case Permisos.AsociarTiraCajeroRed : nomePermiso = ContractoServicio.Constantes.C_PERMISO_ASIGNAR_FORMATO_MOD_RED
            Case Permisos.MantenerFormatoTira : nomePermiso = ContractoServicio.Constantes.C_PERMISO_FORMATO_TIRA
            Case Permisos.RegistrarTira : nomePermiso = ContractoServicio.Constantes.C_PERMISO_REGISTRAR_TIRA
        End Select

        ' Para cada continente existente
        For Each continente In Parametros.UsuarioLogado.ContinentesPermiso

            ' Para cada país exisntente
            For Each pais In continente.Paises

                ' Para cada delegação existente
                For Each delegacao In pais.Delegaciones

                    For Each planta In DirectCast(delegacao, ContractoServicio.Login.EjecutarLogin.DelegacionPlanta).Plantas

                        ' Para cada setor
                        For Each tiposetor In planta.TiposSectores

                            ' Se encontrou a permiso retorna True
                            ' Se não a permiso retorna False
                            Return tiposetor.Permisos.Find(New Predicate(Of ContractoServicio.Login.EjecutarLogin.Permiso)(Function(p) p.Nombre = nomePermiso)) IsNot Nothing

                        Next

                    Next

                Next

            Next

        Next

        ' Caso não encontre a permissão
        Return False

    End Function

    ''' <summary>
    ''' Método responsável por gravar, em formato XML, o erro no diretório pre-definido
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Sub SerializaErroAplicacao(Peticion As ContractoServicio.LogarErro.Peticion)

        Try

            'Verifica se o diretório padrão de erro da aplicação está criado
            If Not Directory.Exists(LogicaNegocio.Parametros.PathXmlError) Then

                'Cria diretório automaticamente
                Directory.CreateDirectory(LogicaNegocio.Parametros.PathXmlError)

            End If

            'Serializa o objeto Peticio contendo as informações da exceção
            Dim objSerializer As New XmlSerializer(GetType(ContractoServicio.LogarErro.Peticion))

            Using objWriter As New StreamWriter(Parametros.PathXmlError & System.Guid.NewGuid().ToString() & ".xml")
                objSerializer.Serialize(objWriter, Peticion)
                objWriter.Close()
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

End Class
