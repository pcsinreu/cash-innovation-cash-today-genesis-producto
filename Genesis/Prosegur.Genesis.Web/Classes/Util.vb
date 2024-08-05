Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.Comunicacion
Imports System.IO
Imports System.Text
Imports System.Xml.Serialization
Imports System.Runtime.Serialization.Formatters.Binary
Imports System.Threading
Imports Prosegur.Genesis.Web.Login

Public Class Util

    Private Shared _DiretorioCorrente As String = Environment.CurrentDirectory
    Private Shared _DiretorioArquivosBaixado As String = String.Empty

    Public Shared AplicacionesAbertas As New List(Of ContractoServicio.Login.EjecutarLogin.AplicacionVersion)
    Public Shared AtualizouGenesis As Boolean = False


    ''' <summary>
    ''' Retorna o caminho completo do arquivo
    ''' </summary>
    ''' <param name="caminho">Caminho onde se encontra o arquivo</param>
    ''' <param name="nomeArquivo">Nome do arquivo (sem a extensão)</param>
    ''' <param name="extensaoArquivo">Extensão do arquivo</param>
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
    ''' Metodo genérico para logar os erros retornados pelos webservices.
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Sub LogarErroAplicacao(Respuesta As ContractoServicio.RespuestaGenerico)
        'Grava log de Erro
        LogarErroAplicacao(Respuesta, String.Empty)

    End Sub

    ''' <summary>
    ''' Metodo genérico para logar os erros retornados pelos webservices.
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Sub LogarErroAplicacao(Respuesta As ContractoServicio.RespuestaGenerico, OutrasInformacoes As String)

        'Cria objetos Peticion e Respuesta de ContractoServicio.LogarErro
        Dim Peticion As New ContractoServicio.LogarErro.Peticion()
        Dim RespuestaLog As ContractoServicio.LogarErro.Respuesta

        'Preenche parâmetros de entrada para logar erro
        Peticion.FechaError = DateTime.Now

        ' Verifica se a informação do usuário foi preenchida
        If Parametros.Permisos.Usuario IsNot Nothing Then
            Peticion.CodigoUsuario = Parametros.Permisos.Usuario.Login.ToUpper
            Peticion.CodigoDelegacion = Parametros.Permisos.Usuario.CodigoDelegacion
        End If

        Peticion.DescripcionError = String.Format("{0} - {1}", Respuesta.CodigoError, Respuesta.MensajeError)
        Peticion.DescripcionOtro = OutrasInformacoes

        Try

            'Chama o serviço responsável pela gravação do log
            RespuestaLog = Parametros.AgenteComunicacion.RecibirMensaje(Comunicacion.Metodo.LogarErro, Peticion)

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
            If Not Directory.Exists(Parametros.PathXmlError) Then

                'Cria diretório automaticamente
                Directory.CreateDirectory(Parametros.PathXmlError)

            End If

            'Serializa o objeto Peticio contendo as informações da exceção
            Dim serializer As New XmlSerializer(GetType(ContractoServicio.LogarErro.Peticion))
            Using writer As New StreamWriter(Parametros.PathXmlError & System.Guid.NewGuid().ToString() & ".xml")
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


End Class
