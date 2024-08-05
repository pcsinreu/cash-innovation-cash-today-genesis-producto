Imports Prosegur.DbHelper
Imports Prosegur.Framework.Dicionario.Tradutor
Imports System.Data.SqlClient
Imports System.IO
Imports System.Configuration.ConfigurationManager

<Serializable()> _
Public Class Automata

#Region "[VARIÁVEIS]"

    Private _Id As Long
    Private _Formulario As Formulario
    Private _RutaDeTrabajo As String
    Private _Usuario As Usuario
    Private _ArchivosPorTurno As Long
    Private _CentrosProceso As CentrosProceso
    Private _Exportador As Boolean
    Private _Descripcion As String
    Private _EstadoAExportar As EstadoComprobante
    Private _DiasAProcesar As Long
    Private _RutaDeCadena As String
    Private _FormatoExportacion As String
    Private _GrabarLogMensajesEnDiscoDuro As Boolean = False

#End Region

#Region "[OUTRAS VARIAVEIS]"

    Private Diccionario As Dictionary(Of String, String)
    Private ArchivoExportado As StreamWriter
    Private ValorEntrada As New Collection
    Private InformeDatos As String
    Private Estado As String
    Private SubEstado As String
    Private EspecieId_IdSIGII As Collection
    Private EspecieId_IdRBO As Collection
    Private IdSIGII_EspecieId As Collection
    Private IdRBO_EspecieId As Collection
    Private TodosDetallesIdentificados As Boolean
    Private TodosDetallesRecontadosIdentificados As Boolean
    ' define se deve ou não enviar emails de sucesso
    Private EnviaCorreoExitoso As Boolean = IIf(Prosegur.Genesis.Web.Login.Configuraciones.AppSettings("EnviaCorreoExitoso") IsNot Nothing AndAlso Prosegur.Genesis.Web.Login.Configuraciones.AppSettings("EnviaCorreoExitoso").ToString().ToLower() = "1", True, False)

#End Region

#Region "[PROPRIEDADES]"

    Public Property Id() As Long
        Get
            Return _Id
        End Get
        Set(value As Long)
            _Id = value
        End Set
    End Property

    Public Property FormatoExportacion() As String
        Get
            Return _FormatoExportacion
        End Get
        Set(value As String)
            _FormatoExportacion = value
        End Set
    End Property

    Public Property RutaDeCadena() As String
        Get
            Return _RutaDeCadena
        End Get
        Set(value As String)
            _RutaDeCadena = value
        End Set
    End Property

    Public Property EstadoAExportar() As EstadoComprobante
        Get
            If _EstadoAExportar Is Nothing Then
                _EstadoAExportar = New EstadoComprobante
            End If
            Return _EstadoAExportar
        End Get
        Set(value As EstadoComprobante)
            _EstadoAExportar = value
        End Set
    End Property

    Public Property DiasAProcesar() As Long
        Get
            Return _DiasAProcesar
        End Get
        Set(value As Long)
            _DiasAProcesar = value
        End Set
    End Property

    Public Property Descripcion() As String
        Get
            Return _Descripcion
        End Get
        Set(value As String)
            _Descripcion = value
        End Set
    End Property

    Public Property Exportador() As Boolean
        Get
            Return _Exportador
        End Get
        Set(value As Boolean)
            _Exportador = value
        End Set
    End Property

    Public Property CentrosProceso() As CentrosProceso
        Get
            If _CentrosProceso Is Nothing Then
                _CentrosProceso = New CentrosProceso
            End If
            Return _CentrosProceso
        End Get
        Set(value As CentrosProceso)
            _CentrosProceso = value
        End Set
    End Property

    Public Property ArchivosPorTurno() As Long
        Get
            Return _ArchivosPorTurno
        End Get
        Set(value As Long)
            _ArchivosPorTurno = value
        End Set
    End Property

    Public Property Usuario() As Usuario
        Get
            If _Usuario Is Nothing Then
                _Usuario = New Usuario
            End If
            Return _Usuario
        End Get
        Set(value As Usuario)
            _Usuario = value
        End Set
    End Property

    Public Property RutaDeTrabajo() As String
        Get
            Return _RutaDeTrabajo
        End Get
        Set(value As String)
            _RutaDeTrabajo = value
        End Set
    End Property

    Public Property Formulario() As Formulario
        Get
            If _Formulario Is Nothing Then
                _Formulario = New Formulario
            End If
            Return _Formulario
        End Get
        Set(value As Formulario)
            _Formulario = value
        End Set
    End Property

    Public ReadOnly Property RetornaEmail() As String
        Get
            Return InformeDatos
        End Get
    End Property

#End Region

#Region "[METODOS]"

    Public Sub Registrar()

        'PD_AutomataRegistrar
        Dim objTransacao As New Transacao(Constantes.CONEXAO_SALDOS)

        If Me.Id = 0 Then
            Me.Id = Me.ObterIdAutomata()
            Me.RegistrarInsert(objTransacao)
        Else
            Me.RegistrarUpdate(objTransacao)
        End If

        ' Registra os dados dos centros de processos associados ao automata
        RegistrarCentroProcesso(objTransacao)

        objTransacao.RealizarTransacao()

    End Sub

    Private Sub RegistrarInsert(ByRef transacao As Transacao)

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        comando.CommandText = My.Resources.AutomataRegistrarInsert.ToString()
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdAutomata", ProsegurDbType.Inteiro_Longo, Me.Id))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdUsuario", ProsegurDbType.Inteiro_Longo, Me.Usuario.Id))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdFormulario", ProsegurDbType.Inteiro_Longo, Me.Formulario.Id))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "RutaDeTrabajo", ProsegurDbType.Descricao_Curta, Me.RutaDeTrabajo))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdUsuario", ProsegurDbType.Inteiro_Longo, Me.Usuario.Id))

        If Me.ArchivosPorTurno = 0 Then
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "ArchivosPorTurno", ProsegurDbType.Inteiro_Longo, 0))
        Else
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "ArchivosPorTurno", ProsegurDbType.Inteiro_Longo, Me.ArchivosPorTurno))
        End If

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Exportador", ProsegurDbType.Logico, Me.Exportador))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Descripcion", ProsegurDbType.Descricao_Longa, Me.Descripcion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdEstadoAExportar", ProsegurDbType.Inteiro_Longo, Me.EstadoAExportar.Id))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "RutaDeCadena", ProsegurDbType.Descricao_Curta, Me.RutaDeCadena))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "FormatoExportacion", ProsegurDbType.Descricao_Curta, Me.FormatoExportacion))

        If Me.DiasAProcesar = 0 Then
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "DiasAProcesar", ProsegurDbType.Inteiro_Longo, 0))

        Else
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "DiasAProcesar", ProsegurDbType.Inteiro_Longo, Me.DiasAProcesar))
        End If

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Grupo", ProsegurDbType.Descricao_Longa, Constantes.AUTOMATA_ARQUIVO_SEPARADOR))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Orden", ProsegurDbType.Inteiro_Longo, 0))

        transacao.AdicionarItemTransacao(comando)

    End Sub

    Private Sub RegistrarUpdate(ByRef transacao As Transacao)

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        comando.CommandText = My.Resources.AutomataRegistrarUpdate.ToString()
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdAutomata", ProsegurDbType.Inteiro_Longo, Me.Id))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdUsuario", ProsegurDbType.Inteiro_Longo, Me.Usuario.Id))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdFormulario", ProsegurDbType.Inteiro_Longo, Me.Formulario.Id))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "RutaDeTrabajo", ProsegurDbType.Descricao_Curta, Me.RutaDeTrabajo))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdUsuario", ProsegurDbType.Inteiro_Longo, Me.Usuario.Id))

        If Me.ArchivosPorTurno = 0 Then
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "ArchivosPorTurno", ProsegurDbType.Inteiro_Longo, 0))
        Else
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "ArchivosPorTurno", ProsegurDbType.Inteiro_Longo, Me.ArchivosPorTurno))
        End If

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Exportador", ProsegurDbType.Logico, Me.Exportador))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Descripcion", ProsegurDbType.Descricao_Longa, Me.Descripcion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdEstadoAExportar", ProsegurDbType.Inteiro_Longo, Me.EstadoAExportar.Id))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "RutaDeCadena", ProsegurDbType.Descricao_Curta, Me.RutaDeCadena))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "FormatoExportacion", ProsegurDbType.Descricao_Curta, Me.FormatoExportacion))

        If Me.DiasAProcesar = 0 Then
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "DiasAProcesar", ProsegurDbType.Inteiro_Longo, 0))

        Else
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "DiasAProcesar", ProsegurDbType.Inteiro_Longo, Me.DiasAProcesar))
        End If

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Grupo", ProsegurDbType.Descricao_Longa, Constantes.AUTOMATA_ARQUIVO_SEPARADOR))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Orden", ProsegurDbType.Inteiro_Longo, 0))

        transacao.AdicionarItemTransacao(comando)

    End Sub

    Private Function ObterIdAutomata() As Integer

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        comando.CommandText = My.Resources.SAutomata.ToString()
        comando.CommandType = CommandType.Text
        Return CType(AcessoDados.ExecutarScalar(Constantes.CONEXAO_SALDOS, comando), Integer)

    End Function

    Public Sub Realizar()

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        comando.CommandText = My.Resources.AutomataRealizar.ToString()
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdAutomata", ProsegurDbType.Inteiro_Longo, Me.Id))

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            Me.Descripcion = dt.Rows(0)("Descripcion")
            Me.ArchivosPorTurno = dt.Rows(0)("ArchivosPorTurno")
            Me.Descripcion = dt.Rows(0)("Descripcion")
            Me.Formulario.Id = dt.Rows(0)("IdFormulario")
            Me.Formulario.Realizar()
            Me.Usuario.Id = dt.Rows(0)("IdUsuario")
            Me.Usuario.Realizar()
            Me.Exportador = dt.Rows(0)("Exportador")

            If Me.Exportador Then
                CentrosProcesoRealizar()
                Me.EstadoAExportar.Id = dt.Rows(0)("IdEstadoAExportar")
                Me.EstadoAExportar.Realizar()
            End If

            If dt.Rows(0)("RutaDeTrabajo") Is DBNull.Value Then
                Me.RutaDeTrabajo = String.Empty
            Else
                Me.RutaDeTrabajo = dt.Rows(0)("RutaDeTrabajo")
            End If

            If dt.Rows(0)("RutaDeCadena") Is DBNull.Value Then
                Me.RutaDeCadena = String.Empty
            Else
                Me.RutaDeCadena = dt.Rows(0)("RutaDeCadena")
            End If

            If dt.Rows(0)("FormatoExportacion") Is DBNull.Value Then
                Me.FormatoExportacion = String.Empty
            Else
                Me.FormatoExportacion = dt.Rows(0)("FormatoExportacion")
            End If

            If dt.Rows(0)("DiasAProcesar") Is DBNull.Value Then
                Me.DiasAProcesar = 1
            Else
                Me.DiasAProcesar = dt.Rows(0)("DiasAProcesar")
            End If

        End If

    End Sub

    ''' <summary>
    ''' Registra os dados do cento de processo
    ''' </summary>
    ''' <param name="objTransacao">Objeto com a transação</param>
    ''' <remarks></remarks>
    Private Sub RegistrarCentroProcesso(ByRef objTransacao As Transacao)

        ' Se existe centros de processo
        If Me.CentrosProceso IsNot Nothing Then

            ' remove os centros de processo do automata corrente(tabela AutomataCentroProceso)
            Me.CentrosProceso.CentrosProcesoBorrar(Me.Id, objTransacao)

            ' Registras os centros de processos associados au automata
            Me.CentrosProceso.AutomataCentroProcesoRegistrar(Me.Id, objTransacao)

        End If

    End Sub

    ''' <summary>
    ''' Recupera uma lista de centros de processos associados ao automata
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub CentrosProcesoRealizar()

        Me.CentrosProceso.Clear()
        Me.CentrosProceso.RealizarPorAutomata(Me.Id)

    End Sub

    ''' <summary>
    ''' Método que verifica o tipo do Automata (Importação/Exportação) e inicializa o processo.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [maoliveira] 01/06/2009 Criado
    ''' </history>
    Public Sub Trabajar()

        Try

            ' Se exportador
            If Me.Exportador Then
                ' Exportar
                Negocio.Util.LogMensagemEmDisco("        - Exportar")
                Me.Exportar()
            Else
                ' Importar
                Negocio.Util.LogMensagemEmDisco("        - Importar")
                Me.Importar()
            End If

        Catch ex As Exception

            ' Grava o erro no banco
            Util.GravarErroNoBanco(ex, Me.Usuario.Nombre)

            Negocio.Util.LogMensagemEmDisco("***ERRO: " & ex.ToString)

        End Try

    End Sub

    ''' <summary>
    ''' Método que importar os arquivos associados ao automata
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [maoliveira] 02/06/2009 Criado
    ''' </history>
    Public Function Importar() As Integer

        ' Declara as variáveis que serão usadas no método
        Dim ArchivosProcesados As Integer
        Dim objDocumento As Documento = Nothing
        Dim retorno As Boolean

        ' Verifica se o diretorio existe...
        If (Directory.Exists(Me.RutaDeTrabajo & Prosegur.Genesis.Web.Login.Configuraciones.AppSettings("CaminhoEntrada"))) Then

            Negocio.Util.LogMensagemEmDisco("        - Caminho Ruta de Trabajo + CaminhoEntrada: " + Me.RutaDeTrabajo + Prosegur.Genesis.Web.Login.Configuraciones.AppSettings("CaminhoEntrada"))

            ' Recupera os nomes dos arquivos a serem importados
            Dim Archivos As String() = Directory.GetFiles(Me.RutaDeTrabajo & Prosegur.Genesis.Web.Login.Configuraciones.AppSettings("CaminhoEntrada").ToString())

            Util.LogMensagemEmDisco("        - Total Archivos en Directorio: " + Archivos.Count.ToString())
            Util.LogMensagemEmDisco("        - Total Archivos por Turno: " + Me.ArchivosPorTurno.ToString())

            ' Verifica se o número de arquivos que podem ser importados é maior que 0(zero)
            ' Verifica se existe arquivos a serem importados
            If Archivos.Count > 0 AndAlso Me.ArchivosPorTurno > 0 Then

                ' Recupera os dados do formulário
                Me.Formulario.Realizar()

                Util.LogMensagemEmDisco("        - Formulario: " & Me.Formulario.Descripcion)

                ' Recupera o arquivo de dicionário do automata
                Me.CargarDiccionario(True)

                ' Cria a tabela de formatos
                Me.CrearTablasIdEspecie()

                ' Inicializa a quantidade de arquivos Importados
                ArchivosProcesados = 0

                Util.LogMensagemEmDisco("        - Archivos:")

                ' Para cada arquivo
                For Each Archivo As String In Archivos

                    Try

                        ' Atribui verdadeiro a variável de retorno
                        retorno = True

                        ' Incrementa a quantidade de arquivos importados
                        ArchivosProcesados = ArchivosProcesados + 1

                        ' Se a quantidade de arquivos importados é maior do que o permitido finaliza a importação
                        If ArchivosProcesados > Me.ArchivosPorTurno Then Exit For

                        If EnviaCorreoExitoso Then
                            ' Recupera o nome do arquivo processado
                            Me.InformeDatos &= vbCrLf & String.Format(Traduzir("msg_arquivo_carregado"), Right(Archivo, Archivo.Length - Archivo.LastIndexOf("\") - 1))
                        End If

                        ' Define o valor inicial do estado
                        Me.Estado = Constantes.AUTOMATA_ESTADO_NOAGREGADO

                        ' Define o valor inicial do subestado
                        Me.SubEstado = String.Empty

                        ' Cria um novo documento
                        objDocumento = New Documento

                        ' Atribui os valores do formulário ao documento
                        objDocumento.Formulario = Me.Formulario

                        Util.LogMensagemEmDisco("           - Nombre: " & Right(Archivo, Archivo.Length - Archivo.LastIndexOf("\") - 1))

                        ' Recupera os dados do arquivo
                        Me.LeerArchivo(Archivo)

                        ' Configura os dados recuperados do arquivo
                        Me.EstructurarDatos(objDocumento)

                        ' Verifica se existem dados mínimos para processar
                        If Me.MinimosDatosValidosProcesar(objDocumento) Then

                            Util.LogMensagemEmDisco("           - Existem Dados Mínimos para Processamento (CentroProcesoOrigen): SIM")

                            ' Se o formulário é uma Ata de Processo
                            If Me.Formulario.EsActaProceso Then

                                Util.LogMensagemEmDisco("           - Es Acta Proceso.")

                                ' Cria a Ata
                                retorno = CrearActa(objDocumento)
                            End If

                            ' Se criou a Ata
                            If (retorno) Then

                                ' Grava os dados do documento no banco de dados
                                retorno = Me.ConformarFormulario(objDocumento)

                                ' Se gravou documento
                                If (retorno) Then

                                    ' Define o valor para o Estado
                                    Me.Estado = Constantes.AUTOMATA_ESTADO_AGREGADO

                                    ' Define o valor para o SubEstado
                                    Me.SubEstado = Constantes.AUTOMATA_SUBESTADO_ENPROCESSO

                                    ' Verifica se existem dados mínimos para imprimir
                                    If (Me.MinimosDatosValidosImprimir(objDocumento)) Then

                                        Util.LogMensagemEmDisco("           - Imprimir Documento.")

                                        ' Imprime os dados do documento
                                        retorno = Me.ImprimirDocumento(objDocumento)

                                        ' Se imprimiu
                                        If retorno Then

                                            ' Define os valores dos atributos dos automatas
                                            Me.SubEstado = Constantes.AUTOMATA_SUBESTADO_IMPRESO

                                            ' Verifica se o automota possui rota de cadeia
                                            If Me.RutaDeCadena <> String.Empty Then

                                                Util.LogMensagemEmDisco("           - Tiene Rota de Cadeia")

                                                ' Gera o arquivo de cadeia
                                                Me.GenerarArchivoCadena(objDocumento, Archivo)

                                            End If

                                        End If

                                    End If

                                Else
                                    ' Neste caso o Número Externo presente no arquivo, 
                                    ' já existe num dos documentos existentes no banco.
                                    ' Como este arquivo não deve ser processado novamente
                                    ' a variável de retorno é setada para 'True'
                                    retorno = True
                                End If

                            End If
                        Else
                            Util.LogMensagemEmDisco("           - Existem Dados Mínimos para Processamento (CentroProcesoOrigen): NÃO")
                        End If

                        If EnviaCorreoExitoso Then
                            ' Adiciona uma linha no log de mensagens
                            Me.InformeDatos &= vbCrLf
                        End If

                        Util.LogMensagemEmDisco("           - Estado: " & Me.Estado)
                        If Not String.IsNullOrEmpty(Me.SubEstado) Then
                            Util.LogMensagemEmDisco("           - SubEstado: " & Me.SubEstado)
                        End If

                        ' move o arquivo para que não seja mais processado
                        Me.MoverArchivo(Archivo)

                    Catch ex As Exception

                        Util.LogMensagemEmDisco("       *** ERRO: " & ex.Message())

                        ' Define o valor para o Estado
                        Me.Estado = Constantes.AUTOMATA_ESTADO_NOAGREGADO

                        Me.MoverArchivo(Archivo)

                    End Try

                Next


            End If
        Else
            Throw New Exception("Camino no existe: " & Me.RutaDeTrabajo & Prosegur.Genesis.Web.Login.Configuraciones.AppSettings("CaminhoEntrada"))
        End If

    End Function

    ''' <summary>
    ''' Método que inicia o processo de exportação do automata
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [maoliveira] 01/06/2009 Criado
    ''' </history>
    Public Sub Exportar()

        Try
            Dim lstDocumentos As Negocio.Documentos
            Dim archivosProcesados As Long

            ' Verifica se o número de arquivos por turno do automata é maior do que 0 (zero)
            If Me.ArchivosPorTurno > 0 Then

                ' Recupera os documentos do automata
                lstDocumentos = DocumentosRealizar()

                Negocio.Util.LogMensagemEmDisco("        - Documentos: " & lstDocumentos.Count.ToString())

                ' Verifica se o automata possui documentos
                If lstDocumentos.Count > 0 Then

                    ' Recupera o arquivo de dicionário do automata
                    CargarDiccionario(True)

                    ' Cria a tabela de formatos
                    CrearTablasIdEspecie()

                    ' Inicializa a quantidade de arquivos exportados
                    archivosProcesados = 0

                    For Each objDocumento In lstDocumentos

                        ' Recupera os dados do documento
                        LeerDocumento(objDocumento)

                        ' Escreve os dados do documento no arquivo
                        EscribirArchivoExportado(objDocumento)

                        ' Altera o status do documento como exportado
                        objDocumento.SeaExportado()

                        Negocio.Util.LogMensagemEmDisco("        - Cambiando estatos del documento para exportado.")

                        ' Incrementa o número de arquivo processados
                        archivosProcesados = archivosProcesados + 1

                        ' Caso a quantidade seja superior ao permitido, finaliza a exportação
                        If archivosProcesados >= Me.ArchivosPorTurno Then Exit For

                    Next

                    Negocio.Util.LogMensagemEmDisco("        - Total de archivo procesados: " & archivosProcesados.ToString())

                End If

            End If
        Catch ex As Exception
            ' Lança a exceção para o método que o referencia
            Throw
        End Try

    End Sub

    ''' <summary>
    ''' Gera o arquivo de cadeia
    ''' </summary>
    ''' <param name="objDocumento">Dados do documento</param>
    ''' <param name="CaminhoArchivo">Caminho do arquivo</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [maoliveira] 02/06/2009 Criado
    ''' </history>
    Sub GenerarArchivoCadena(objDocumento As Documento, CaminhoArchivo As String)

        ' Variáveis usadas para criar o arquivo
        Dim ArchivoCadena As StreamWriter

        ' Recupera o nome do arquivo
        Dim NomeArchivo As String = Right(CaminhoArchivo, CaminhoArchivo.Length - CaminhoArchivo.LastIndexOf("\") - 1)

        ' Se existe nome
        If (NomeArchivo.IndexOf(".") > 0) Then

            ' Se não existe
            If (Not Directory.Exists(Me.RutaDeCadena)) Then
                ' Cria o diretório
                Directory.CreateDirectory(Me.RutaDeCadena)
            End If

            ' Se existe
            If (File.Exists(Me.RutaDeCadena & NomeArchivo)) Then
                ' Apaga o arquivo
                File.Delete(Me.RutaDeCadena & NomeArchivo)
            End If

            Util.LogMensagemEmDisco("           - Copia Archivo Cadeia ORIGEM: " + CaminhoArchivo)
            Util.LogMensagemEmDisco("           - Copia Archivo Cadeia DESTINO: " + Me.RutaDeCadena + NomeArchivo)

            ' Copia o arquivo para outra pasta
            File.Copy(CaminhoArchivo, Me.RutaDeCadena & NomeArchivo)

            ' Abre o arquivo 
            ArchivoCadena = File.AppendText(Me.RutaDeCadena & NomeArchivo)

            ' Escreve o código do documento
            ArchivoCadena.WriteLine(Constantes.AUTOMATA_ARQUIVO_LINHA_VALOR_INICIAL & _
                                    Constantes.AUTOMATA_ARQUIVO_SEPARADOR & _
                                    Constantes.AUTOMATA_ARQUIVO_CHAVE_IDDOCET & _
                                    Constantes.AUTOMATA_ARQUIVO_SEPARADOR & objDocumento.Id)

            ' Fecha o arquivo
            ArchivoCadena.Close()

            ' Libera o arquivo da memoria
            ArchivoCadena.Dispose()

        End If

    End Sub

    ''' <summary>
    ''' Recupera os Documentos de acordo com os filtros passados como parâmetro.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [maoliveira] 01/06/2009 Criado
    ''' </history>
    Private Function DocumentosRealizar() As Documentos
        ' Variáveis usados para recuperar os dados do documento
        Dim objDocumento As Negocio.Documento
        Dim lstDocumentos As Negocio.Documentos = New Documentos()

        ' Recupera os centros de processo associados ao Automata
        Me.CentrosProcesoRealizar()

        ' Concatena os centros de processo com o caracter "|".
        ' Ex: |356|445|
        Dim strCentrosProceso As String = ""
        For Each cp In Me.CentrosProceso
            If Not String.IsNullOrEmpty(strCentrosProceso) Then
                strCentrosProceso &= ","
            End If
            strCentrosProceso &= "'" & cp.Id & "'"
        Next

        ' Define a conexão a ser usada
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        ' Define o script a ser executado
        comando.CommandText = My.Resources.AutomataDocumentoRealizar.ToString()
        ' Define o tipo do script
        comando.CommandType = CommandType.Text

        ' Define os parametros
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdFormulario", ProsegurDbType.Inteiro_Curto, Me.Formulario.Id))
        'comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdCentrosProceso", ProsegurDbType.Observacao_Curta, strCentrosProceso))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdEstadoAExportar", ProsegurDbType.Inteiro_Curto, Me.EstadoAExportar.Id))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "DiasAProcesar", ProsegurDbType.Inteiro_Longo, Me.DiasAProcesar))
        If Not String.IsNullOrEmpty(strCentrosProceso) Then
            comando.CommandText &= " AND IdCentroProcesoDestino in (" & strCentrosProceso & ")"
        End If

        ' Recupera os documento
        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)

        'Para cada documento encontrado
        For Each dr As DataRow In dt.Rows
            ' Grava os dados do documento
            objDocumento = New Documento()
            objDocumento.Id = dr("Id")
            objDocumento.EstadoComprobante = New EstadoComprobante()
            objDocumento.EstadoComprobante.Id = dr("IdEstadoComprobante")
            lstDocumentos.Add(objDocumento)
        Next

        ' Retorna uma lista de documentos
        Return lstDocumentos

    End Function

    ''' <summary>
    ''' Este método cria uma tabela de espécies (formatos), que será usada posteriormente para a configuração do arquivo a ser exportado, 
    ''' atualmente existem dois formatos SIGII e RBO
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [maoliveira] 01/06/2009 Criado
    ''' </history>
    Private Sub CrearTablasIdEspecie()

        ' Recupera os formatos existentes
        Dim Especies As New Especies()
        Especies.Realizar(True)

        If Especies.Count > 0 Then

            'Cria as variaveis de formatos
            EspecieId_IdSIGII = New Collection
            EspecieId_IdRBO = New Collection
            IdSIGII_EspecieId = New Collection
            IdRBO_EspecieId = New Collection

            ' Para cada especie (formato)
            For Each especie In Especies

                If Not (EspecieId_IdRBO.Contains(especie.IdRBO) AndAlso EspecieId_IdSIGII.Contains(especie.IdSIGII)) Then

                    EspecieId_IdSIGII.Add(especie.Id, especie.IdSIGII)
                    EspecieId_IdRBO.Add(especie.Id, especie.IdRBO)

                    IdSIGII_EspecieId.Add(especie.IdSIGII, CStr(especie.Id))
                    IdRBO_EspecieId.Add(especie.IdRBO, CStr(especie.Id))

                End If

            Next

        End If

    End Sub

    ''' <summary>
    ''' Método que verifica se existem dados mínimos para o processamento
    ''' </summary>
    ''' <param name="objDocumento">Dados do documento</param>
    ''' <returns>Retorna Verdadeiro ou Falso </returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [maoliveira] 02/06/2009 Criado
    ''' </history>
    Private Function MinimosDatosValidosProcesar(objDocumento As Documento) As Boolean

        ' Verifica se a lista de campos do formulário contém CentroProcesoOrigen
        Return (From c In objDocumento.Formulario.Campos _
                Where c.Nombre.Contains(Constantes.AUTOMATA_FORMULARIO_CAMPO_CENTROPROCESOORIGEN)) IsNot Nothing

    End Function

    ''' <summary>
    ''' Método que verifica se existmem dodos mínimos para a impressão
    ''' </summary>
    ''' <param name="objDocumento">Dados do documento</param>
    ''' <returns>Retorna Verdadeiro ou Falso </returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [maoliveira] 02/06/2009 Criado
    ''' </history>
    Private Function MinimosDatosValidosImprimir(objDocumento As Documento) As Boolean

        'Variáveis usadas no método
        Dim Logica As Boolean = False

        ' Recupera da lista de campos do formulário o campo CentroProcesoOrigen
        Dim objCampo = (From c In objDocumento.Formulario.Campos _
                 Where c.Nombre.Contains(Constantes.AUTOMATA_FORMULARIO_CAMPO_CENTROPROCESOORIGEN) _
                 AndAlso c.IdValor <> 0)

        ' Verifica se o formulário possui CentroProcesoOrigen 
        Logica = objCampo IsNot Nothing AndAlso objCampo.Count > 0

        ' Recupera da lista de campos do formulário o campo  Banco
        objCampo = (From c In objDocumento.Formulario.Campos _
                 Where c.Nombre.Contains(Constantes.AUTOMATA_FORMULARIO_CAMPO_BANCO) _
                 AndAlso c.IdValor <> 0)

        ' Verifica se o formulário possui Banco 
        Logica = Logica AndAlso objCampo IsNot Nothing AndAlso objCampo.Count > 0

        ' Recupera da lista de campos do formulário o campo ClienteOrigen
        objCampo = (From c In objDocumento.Formulario.Campos _
                 Where c.Nombre.Contains(Constantes.AUTOMATA_FORMULARIO_CAMPO_CLIENTEORIGEN) _
                 AndAlso c.IdValor <> 0)

        ' Verifica se o formulário possui ClienteOrigen 
        Logica = Logica AndAlso objCampo IsNot Nothing AndAlso objCampo.Count > 0

        ' Verifica se o formulário não é uma Ata de Processo e Todos os detalhes foram identificados 
        ' OU se o formulário é uma Ata de Processo e Todos os detalhes recontados foram identificados
        Logica = Logica AndAlso ((Not objDocumento.Formulario.EsActaProceso AndAlso TodosDetallesIdentificados) _
                OrElse (objDocumento.Formulario.EsActaProceso AndAlso TodosDetallesRecontadosIdentificados))

        ' Retorna a verdadeiro ou falso
        Return Logica

    End Function

    ''' <summary>
    ''' Método usado para imprimir o documento
    ''' </summary>
    ''' <param name="objDocumento">Dados do documeno</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [maoliveira] 02/06/2009 Criado
    ''' </history>
    Private Function ImprimirDocumento(objDocumento As Documento) As Boolean

        ' Variáveis usadas para impressão
        Dim mensaje As String
        Dim retornoImprimir As Integer = 0
        Dim retorno As Boolean = False

        ' --------------------------------------------------------------------------------------------------
        ' Os comandos abaixo, existiam no metodo ImprimirDocumento original e foram removidos, porque quando
        ' uma nova instância de documento é criado, a propriedade 'ConValores' do 'Formulario' sempre é
        ' 'false', ou seja, ele sempre vai imprimir o documento.
        '
        'Doc = sCtxtCreateObject("ProseDocs.Documento", octxt)
        'Doc.Id = CoLng(requestForm("Id"))
        'If (Doc.Formulario.ConValores AndAlso Doc.Detalles.Count > 0) OrElse (Not Doc.Formulario.ConValores) Then
        ' -------------------------------------------------------------------------------------------------

        ' Imprime o documento
        retornoImprimir = objDocumento.Imprimir()

        ' Verifica se aconteceu algum erro
        If retornoImprimir = 0 Then
            ' Define mensagem de sucesso
            mensaje = String.Format(Traduzir("msg_documento_imprimir_sucesso"), objDocumento.Id)
            retorno = True
        Else
            ' Define mensagem de erro
            mensaje = Traduzir("msg_documento_imprimir_erro")
        End If

        If EnviaCorreoExitoso AndAlso retorno Then
            InformeDatos &= vbCrLf & mensaje
        End If

        Return retorno

    End Function

    ''' <summary>
    ''' Método usado gravar os dados do documento no banco
    ''' </summary>
    ''' <param name="objDocumento">Dados do documento</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [maoliveira] 03/06/2009 Criado
    ''' </history>
    Private Function ConformarFormulario(ByRef objDocumento As Documento) As Long

        Dim retorno As Boolean = True

        Dim cp = New Negocio.CentroProceso
        Dim idCP As Negocio.Campo = objDocumento.Formulario.Campos.FirstOrDefault(Function(f) f.Nombre = Constantes.AUTOMATA_FORMULARIO_CAMPO_CENTROPROCESOORIGEN)
        cp.Id = If(idCP IsNot Nothing, idCP.IdValor, 0)
        cp.Realizar()
        cp.Planta.Realizar()
        objDocumento.Fecha = Util.GetDateTime(cp.Planta.CodDelegacionGenesis)
        objDocumento.GMTVeranoAjuste = If(Not String.IsNullOrEmpty(cp.Planta.CodDelegacionGenesis), CType(Util.GetGMTVeranoAjuste(cp.Planta.CodDelegacionGenesis), Short?), Nothing)

        ' Configura os dados do documento
        objDocumento.EsGrupo = False
        objDocumento.Agrupado = False
        objDocumento.Grupo.Id = 0
        objDocumento.Sustituido = False
        objDocumento.EsSustituto = False
        'objDocumento.Fecha = DateTime.Now 'siempre que guardamos actualizamos a la fecha y hora actual
        objDocumento.Usuario = Me.Usuario

        ' Registra os dados do documento
        Dim msgRetorno As List(Of String) = objDocumento.Registrar(False)

        ' Define a mensagem e o retorno
        If (objDocumento.Id <> 0) Then

            If EnviaCorreoExitoso Then
                InformeDatos &= vbCrLf & String.Format(Traduzir("msg_documento_salvar_sucesso"), objDocumento.Id)
            End If

            Util.LogMensagemEmDisco("           - " & String.Format(Traduzir("msg_documento_salvar_sucesso"), objDocumento.Id))

        Else

            For Each msg In msgRetorno

                If EnviaCorreoExitoso Then

                    InformeDatos &= vbCrLf & msg

                End If

                Util.LogMensagemEmDisco("           - " & msg)

            Next

            retorno = False

        End If

        Return retorno

    End Function

    ''' <summary>
    ''' Este método cria um dicionário, que será usado posteriormente para a configuração do arquivo a ser exportado, 
    ''' Este dicionário possui a origem e o destino de campo
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [maoliveira] 01/06/2009 Criado
    ''' </history>
    Private Sub CargarDiccionario(Optional Exportando As Boolean = False)

        Try

            ' Recupera o caminho e o nome do arquivo de dicionário
            Dim nomeArquivo As String = Me.RutaDeTrabajo & Prosegur.Genesis.Web.Login.Configuraciones.AppSettings("NomeArquivoDicionario").ToString()

            ' Verifica se o arquivo existe
            If (File.Exists(nomeArquivo)) Then

                ' Abre o arquivo
                Dim fsDicionario As StreamReader = File.OpenText(nomeArquivo)

                ' Cria um novo dicionário
                Diccionario = New Dictionary(Of String, String)

                ' Variaveis usada para manipulação dos dados
                Dim linea, campoOrigen, campoDestino As String

                ' Enquanto existir linha no arquivo
                Do While Not fsDicionario.EndOfStream
                    ' Recupera os dados de uma linha do arquivo
                    linea = fsDicionario.ReadLine
                    ' Recupera o Campo de Origem
                    campoOrigen = Left(linea, InStr(linea, Constantes.AUTOMATA_ARQUIVO_SEPARADOR) - 1)
                    ' Recupera o Campo de Destino
                    campoDestino = Right(linea, Len(linea) - InStr(linea, Constantes.AUTOMATA_ARQUIVO_SEPARADOR))
                    ' Verifica se o Automata vai ser exportado
                    If Me.Exportador Then
                        ' Adiciona os dados do dicionário
                        Diccionario.Add(campoOrigen, campoDestino)
                    Else
                        ' Adiciona os dados do dicionário invertido
                        Diccionario.Add(campoDestino, campoOrigen)
                    End If
                Loop
                ' Fecha o arquivo lido
                fsDicionario.Close()
                ' Limpa o arquivo da memoria
                fsDicionario.Dispose()
            End If
        Catch ex As Exception

            ' Adiciona uma mensagem informando que não foi possível carregar o arquivo do automata
            InformeDatos &= vbCrLf & Traduzir("msg_dicionario_nao_encontrado")

            ' Lança a exceção para o método que o referencia
            Throw

        End Try

    End Sub

    ''' <summary>
    ''' Método para mover o arquivo
    ''' </summary>
    ''' <param name="CaminhoArchivo">Caminho do arquivo</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [maoliveira] 01/06/2009 Criado
    ''' </history>
    Sub MoverArchivo(CaminhoArchivo As String)

        Util.LogMensagemEmDisco("           - Cambiando archivo.")
        Util.LogMensagemEmDisco("           - Camino archivo ORIGEM: " + CaminhoArchivo)

        ' Recupera o nome do arquivo
        Dim NomeArchivo As String = Right(CaminhoArchivo, CaminhoArchivo.Length - CaminhoArchivo.LastIndexOf("\") - 1)

        ' Se existe arquivo
        If (File.Exists(CaminhoArchivo)) Then

            ' Se é nome
            If (NomeArchivo.IndexOf(".") > 0) Then

                ' Define o caminho de destino do arquivo
                Dim ruta As String = Me.RutaDeTrabajo & Estado & "\"
                If Not String.IsNullOrEmpty(SubEstado) Then
                    ruta &= SubEstado + "\"
                End If

                'Se não existe
                If (Not Directory.Exists(ruta)) Then
                    Directory.CreateDirectory(ruta)
                End If

                ' Adiciona o nome do arquivo na rota
                ruta &= NomeArchivo

                ' Se arquivo existe
                If (File.Exists(ruta)) Then
                    ' Apaga o arquivo
                    File.Delete(ruta)
                End If

                Util.LogMensagemEmDisco("           - Camino archivo DESTINO: " + ruta)

                ' Move o arquivo
                File.Move(CaminhoArchivo, ruta)

            End If

        End If

    End Sub

    ''' <summary>
    ''' Método que grava um log do arquivo
    ''' </summary>
    ''' <param name="CaminhoArchivo">Caminho do arquivo</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [maoliveira] 01/06/2009 Criado
    ''' </history>
    Sub GenerarLog(CaminhoArchivo As String)
        ' Variáveis usadas para gravação do arquivo de Log
        Dim ArchivoLog As StreamWriter, ruta As String
        ' Recupera o nome do arquivo
        Dim NomeArchivo As String = Right(CaminhoArchivo, CaminhoArchivo.Length - CaminhoArchivo.LastIndexOf("\") - 1)

        ' Se existe nome
        If (NomeArchivo.IndexOf(".") > 0) Then

            ' Verifica o estado do Automata
            If Me.Estado = Constantes.AUTOMATA_ESTADO_AGREGADO Then
                ruta = Me.RutaDeTrabajo & Estado & "\" + SubEstado & "\"
            Else
                ruta = Me.RutaDeTrabajo & Estado & "\"
            End If

            ' Se não existe
            If Not (Directory.Exists(ruta)) Then
                ' Cria o diretório
                Directory.CreateDirectory(ruta)
            End If

            ' Abre o arquivo
            ArchivoLog = File.CreateText(ruta & Left(NomeArchivo, Len(NomeArchivo) - 4) & Constantes.AUTOMATA_EXTENSAO_ARQUIVO_LOG)
            ' Grava o log
            ArchivoLog.Write(InformeDatos)
            ' Fecha o arquivo
            ArchivoLog.Close()
            ' Libera o arquivo da memoria
            ArchivoLog.Dispose()
        End If
    End Sub

    ''' <summary>
    ''' Método que lê os dados do arquivo.
    ''' </summary>
    ''' <param name="NomeArchivo">Nome do arquivo a ser lido.</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [maoliveira] 02/06/2009 Criado
    ''' </history>
    Sub LeerArchivo(NomeArchivo As String)

        ValorEntrada = New Collection()

        ' Cria as coleções padrões
        ValorEntrada.Add(New Collection, Constantes.AUTOMATA_ARQUIVO_CHAVE_MONTOXX)
        ValorEntrada.Add(New Collection, Constantes.AUTOMATA_ARQUIVO_CHAVE_DETALLE)
        ValorEntrada.Add(New Collection, Constantes.AUTOMATA_ARQUIVO_CHAVE_BOLPREC)

        ' Se existe
        If (NomeArchivo <> String.Empty) Then

            ' Abre o arquivo
            Dim fsArquivoEntrada As StreamReader = File.OpenText(NomeArchivo)

            ' Variaveis usada para manipulação dos dados
            Dim Linea, CampoOrigen, Valor As String

            ' Enquanto existir linha no arquivo
            Do While Not fsArquivoEntrada.EndOfStream
                ' Recupera os dados de uma linha do arquivo
                Linea = fsArquivoEntrada.ReadLine
                ' Recupera o campo de origem
                CampoOrigen = Mid(Linea, InStr(Linea, Constantes.AUTOMATA_ARQUIVO_SEPARADOR) + 1, 7)
                ' Se exitir valor
                If (Len(Linea) - InStr(Linea, Constantes.AUTOMATA_ARQUIVO_SEPARADOR) - 8) > 0 Then
                    ' Recupera o o valor do campo
                    Valor = Right(Linea, Len(Linea) - InStr(Linea, Constantes.AUTOMATA_ARQUIVO_SEPARADOR) - 8)
                    ' Verifica o campo de origem
                    Select Case CampoOrigen

                        Case Constantes.AUTOMATA_ARQUIVO_CHAVE_MONTOXX, Constantes.AUTOMATA_ARQUIVO_CHAVE_DETALLE, Constantes.AUTOMATA_ARQUIVO_CHAVE_BOLPREC
                            ' Caso seja um dos campos padrões, adiciona o valor na coleção deles
                            Dim Dados As Collection = ValorEntrada.Item(CampoOrigen)
                            Dados.Add(Valor)
                        Case Else
                            ' Caso não seja um dos campos padrões, adiciona uma nova coleção
                            ' Se o campo não existe na lista de campos carregados
                            If Not (ValorEntrada.Contains(CampoOrigen)) Then
                                ' Adiciona o campo na lista
                                ValorEntrada.Add(Valor, CampoOrigen)
                            End If
                    End Select
                Else
                    ' Se não existir valor 
                    If CampoOrigen <> vbNullString Then
                        ' Adiciona uma string vazia para o valor
                        ValorEntrada.Add(vbNullString, CampoOrigen)
                    End If
                End If
            Loop

            ' Fecha o arquivo
            fsArquivoEntrada.Close()

            'Libera o arquivo da memoria
            fsArquivoEntrada.Dispose()

        End If

    End Sub

    ''' <summary>
    ''' Método que recupera os dados do arquivo
    ''' </summary>
    ''' <param name="objDocumento">Dados do documento</param>
    ''' <remarks></remarks>
    ''' <hitory>
    ''' [maoliveira] 02/06/2009 Criado
    ''' </hitory>
    Private Sub EstructurarDatos(ByRef objDocumento As Documento)

        ' Se é ata de Processo
        If Me.Formulario.EsActaProceso Then
            'Estrutura o código do Docucento
            Me.EstructurarIdOrigen(objDocumento)
        End If

        ' Recupera o número externo e a data de gestão
        Me.EstructurarNumExternoFechaGestion(objDocumento)

        ' Recupera o centro de processo
        Me.EstructurarCentroProceso(objDocumento)

        ' Verifica se existem o campo 'CentroProcesoDestino'
        Dim objCampoCPD = From c In Me.Formulario.Campos _
                          Where c.Nombre.Contains(Constantes.AUTOMATA_FORMULARIO_CAMPO_CENTROPROCESODESTINO)

        ' Se Existir
        If objCampoCPD IsNot Nothing AndAlso objCampoCPD.Count > 0 Then

            ' Recupera o Centro de Processo Destino
            Me.EstructurarCentroProcesoDestino(objDocumento)

            ' Recupera o IdOrigem de acordo com o Número externo
            Me.EstructurarIdOrigenDesdeNumExterno(objDocumento)

        End If

        ' Recupera o banco, o cliente e a sucursal
        Me.EstructurarBancoClienteSucursal(objDocumento)

        ' Verifica se existe o campo 'ClienteDestino' ou 'BancoDeposito'
        Dim objCampoCB = From c In Me.Formulario.Campos _
                          Where c.Nombre.Contains(Constantes.AUTOMATA_FORMULARIO_CAMPO_CLIENTEDESTINO) _
                          OrElse c.Nombre.Contains(Constantes.AUTOMATA_FORMULARIO_CAMPO_BANCODEPOSITO)

        ' Se Existir
        If objCampoCB IsNot Nothing AndAlso objCampoCB.Count > 0 Then
            ' Recupera o banco, o cliente e a sucursal destino
            Me.EstructurarBancoClienteSucursalDestino(objDocumento)
        End If

        ' Recupera os campos Extras
        Me.EstructurarCamposExtra(objDocumento)

        ' Recupera os pacotes
        Me.EstructurarBultos(objDocumento)

        ' Se é ata de processo
        If Me.Formulario.EsActaProceso Then
            ' Recupera os detalhes recontados
            Me.EstructurarDetallesRecontado(objDocumento)
        Else
            ' Recupera os detalhes
            Me.EstructurarDetalles(objDocumento)
        End If

    End Sub

    ''' <summary>
    ''' Método que recupera os dados do campos extras
    ''' </summary>
    ''' <param name="objDocumento">Dados do documento</param>
    ''' <remarks></remarks>
    ''' <hitory>
    ''' [maoliveira] 02/06/2009 Criado
    ''' </hitory>
    Private Sub EstructurarCamposExtra(ByRef objDocumento As Documento)
        ' Cria a variavel campo extra usada na interação
        Dim campoExtra As CampoExtra

        ' Se o dicionário está preenchido
        If (Diccionario IsNot Nothing AndAlso Diccionario.Count > 0) Then
            ' Para cada campo extra
            For Each campoExtra In objDocumento.Formulario.CamposExtra
                ' Verifica se o campo extra existe no dicionario
                Dim objCampo = From c In Diccionario _
                               Where c.Key.Contains(campoExtra.Nombre)
                ' Se Existir
                If objCampo IsNot Nothing AndAlso objCampo.Count > 0 Then
                    ' Verifica se a chave existe no valorEntrada
                    If (ValorEntrada.Contains(Diccionario(campoExtra.Nombre))) Then
                        ' Recupera o valor do campo extra
                        campoExtra.Valor = ValorEntrada(Diccionario(campoExtra.Nombre))
                    End If
                End If
            Next
        End If
    End Sub

    ''' <summary>
    ''' Método que recupera os pacotes
    ''' </summary>
    ''' <param name="objDocumento">Dados do documento</param>
    ''' <remarks></remarks>
    ''' <hitory>
    ''' [maoliveira] 02/06/2009 Criado
    ''' </hitory>
    Private Sub EstructurarBultos(ByRef objDocumento As Documento)
        ' Variáveis usadas para recuperar os dados
        Dim DESTINO As String
        Dim objBulto As Bulto
        Dim CodBolsa, NumPrecinto, DestinoId As String

        ' Se a chave existe na lista de valores de entrada
        If (ValorEntrada.Contains(Constantes.AUTOMATA_ARQUIVO_CHAVE_DESTINO)) Then

            ' Recupera o valor da chave DESTINO
            DESTINO = ValorEntrada(Constantes.AUTOMATA_ARQUIVO_CHAVE_DESTINO)
            DestinoId = Trim(Mid$(DESTINO, 1, 1))

            ' Se a chave existe na lista de valores de entrada
            If (ValorEntrada.Contains(Constantes.AUTOMATA_ARQUIVO_CHAVE_BOLPREC)) Then

                ' Para cada item, recupera o valor da chave BOLPREC
                For Each Item In ValorEntrada(Constantes.AUTOMATA_ARQUIVO_CHAVE_BOLPREC)
                    ' Recupera o código da bolsa
                    CodBolsa = Trim(Mid$(Item, 1, 10))
                    ' Recupera o número do precinto
                    NumPrecinto = Trim(Mid$(Item, 12, 10))

                    ' Adiciona os dados na lista de pacotes do documento
                    objBulto = New Bulto()
                    objBulto.NumPrecinto = NumPrecinto
                    objBulto.CodBolsa = CodBolsa
                    objBulto.Destino.Id = DestinoId
                    objDocumento.Bultos.Add(objBulto)
                Next

            End If

        End If

    End Sub

    ''' <summary>
    ''' Método que recupera os valors dos detalhes
    ''' </summary>
    ''' <param name="objDocumento">Dados do documento</param>
    ''' <remarks></remarks>
    ''' <hitory>
    ''' [maoliveira] 02/06/2009 Criado
    ''' </hitory>
    Private Sub EstructurarDetalles(ByRef objDocumento As Documento)
        ' Variáveis usadas para recuperar os dados
        Dim AplicacionOrigen As String = String.Empty
        Dim IdAPL As String = String.Empty
        Dim strImporte, Cantidad, ImporteEntera, ImporteDecimal, Importe As String
        Dim EspecieId As String = String.Empty
        Dim objDetalhe As Detalle

        ' Se a chave existe na lista de valores de entrada
        If (ValorEntrada.Contains(Constantes.AUTOMATA_ARQUIVO_CHAVE_VERSION)) Then
            ' verifica se o valor da chave VERSION
            If Left(ValorEntrada(Constantes.AUTOMATA_ARQUIVO_CHAVE_VERSION), 5) = Constantes.AUTOMATA_ARQUIVO_VERSAO_SIGII Then
                ' Recupera o formato do arquivo
                AplicacionOrigen = Constantes.AUTOMATA_ARQUIVO_VERSAO_SIGII
            End If
            ' verifica se o valor da chave VERSION
            If Left(ValorEntrada(Constantes.AUTOMATA_ARQUIVO_CHAVE_VERSION), 3) = Constantes.AUTOMATA_ARQUIVO_VERSAO_RBO Then
                ' Recupera o formato do arquivo
                AplicacionOrigen = Constantes.AUTOMATA_ARQUIVO_VERSAO_RBO
            End If
        End If

        ' Inicializa a váriavel de todos os detalhes identificados para verdadeiro
        TodosDetallesIdentificados = True

        ' Se a chave existe na lista de valores de entrada
        If (ValorEntrada.Contains(Constantes.AUTOMATA_ARQUIVO_CHAVE_MONTOXX)) Then

            Try
                'Para cada entrada dentro da chave MONTOXX
                For Each entrada As String In ValorEntrada(Constantes.AUTOMATA_ARQUIVO_CHAVE_MONTOXX)
                    ' Recupera a chave do formato
                    IdAPL = Trim(Left(entrada, 7))

                    ' Verifica o formato do arquivo
                    Select Case AplicacionOrigen
                        Case Constantes.AUTOMATA_ARQUIVO_VERSAO_SIGII
                            ' Recupera o valor da chave
                            EspecieId = EspecieId_IdSIGII(IdAPL)
                        Case Constantes.AUTOMATA_ARQUIVO_VERSAO_RBO
                            ' Recupera o valor da chave
                            EspecieId = EspecieId_IdRBO(IdAPL)
                    End Select

                    ' Recupera a quantidade
                    Cantidad = Mid$(entrada, 9, 14)
                    ' Se quatindade for zero
                    If CLng(Cantidad) = 0 Then
                        ' Quantidade recebe 1
                        Cantidad = 1
                    End If

                    ' Recupera o valor da importancia
                    strImporte = Mid$(entrada, 25, 17)
                    ' Recupera a parte inteira
                    ImporteEntera = Left(strImporte, 14)
                    ' Recupera a parte decimal
                    ImporteDecimal = Right(strImporte, 2)
                    ' Soma a parte inteira com a parte decimal
                    Importe = CLng(ImporteEntera) + CLng(ImporteDecimal) / 100

                    If (Importe <> 0 AndAlso EspecieId <> String.Empty) Then
                        objDetalhe = New Detalle
                        objDetalhe.Cantidad = Cantidad
                        objDetalhe.Importe = Importe
                        objDetalhe.Especie.Id = CInt(EspecieId)
                        objDocumento.Detalles.Add(objDetalhe)
                    End If

                Next
            Catch ex As Exception
                ' Atribui falso para a variável
                TodosDetallesIdentificados = False
                ' Define a mensagem, espécie não identificada concatenado com o formato
                InformeDatos &= vbCrLf & String.Format(Traduzir("msg_especie_nao_identificada"), IdAPL)
            End Try
        End If
    End Sub

    ''' <summary>
    ''' Método que recupera os valores dos detalhes Recontados
    ''' </summary>
    ''' <param name="objDocumento">Dados do documento</param>
    ''' <remarks></remarks>
    ''' <hitory>
    ''' [maoliveira] 02/06/2009 Criado
    ''' </hitory>
    Private Sub EstructurarDetallesRecontado(ByRef objDocumento As Documento)
        ' Variáveis usadas para recuperar os dados
        Dim AplicacionOrigen As String = String.Empty
        Dim IdAPL As String = String.Empty
        Dim strImporte, Cantidad, ImporteEntera, ImporteDecimal, Importe As String
        Dim EspecieId As String = String.Empty
        Dim objDetalhe As Detalle

        ' Se a chave existe na lista de valores de entrada
        If (ValorEntrada.Contains(Constantes.AUTOMATA_ARQUIVO_CHAVE_VERSION)) Then
            ' verifica se o valor da chave VERSION
            If Left(ValorEntrada(Constantes.AUTOMATA_ARQUIVO_CHAVE_VERSION), 5) = Constantes.AUTOMATA_ARQUIVO_VERSAO_SIGII Then
                ' Recupera o formato do arquivo
                AplicacionOrigen = Constantes.AUTOMATA_ARQUIVO_VERSAO_SIGII
            End If
            ' verifica se o valor da chave VERSION
            If Left(ValorEntrada(Constantes.AUTOMATA_ARQUIVO_CHAVE_VERSION), 3) = Constantes.AUTOMATA_ARQUIVO_VERSAO_RBO Then
                ' Recupera o formato do arquivo
                AplicacionOrigen = Constantes.AUTOMATA_ARQUIVO_VERSAO_RBO
            End If
        End If

        ' Inicializa a váriavel de todos os detalhes identificados para verdadeiro
        TodosDetallesRecontadosIdentificados = True

        ' Se a chave existe na lista de valores de entrada
        If (ValorEntrada.Contains(Constantes.AUTOMATA_ARQUIVO_CHAVE_DETALLE)) Then

            Try
                'Para cada entrada dentro da chave DETALLE
                For Each entrada As String In ValorEntrada(Constantes.AUTOMATA_ARQUIVO_CHAVE_DETALLE)
                    ' Verifica o formato do arquivo
                    Select Case AplicacionOrigen
                        Case Constantes.AUTOMATA_ARQUIVO_VERSAO_SIGII
                            ' Recupera a chave do formato
                            IdAPL = Trim(Mid(entrada, 9, 7))
                            ' Recupera o valor da chave
                            EspecieId = EspecieId_IdSIGII(IdAPL)
                        Case Constantes.AUTOMATA_ARQUIVO_VERSAO_RBO
                            ' Recupera a chave do formato
                            IdAPL = Trim(Mid(entrada, 9, 7))
                            ' Recupera o valor da chave
                            EspecieId = EspecieId_IdRBO(IdAPL)
                    End Select

                    ' Recupera a quantidade
                    Cantidad = Mid$(entrada, 26, 14)
                    ' Se quatindade for zero
                    If CLng(Cantidad) = 0 Then
                        ' Quantidade recebe 1
                        Cantidad = 1
                    End If

                    ' Recupera o valor da importancia
                    strImporte = Mid$(entrada, 41, 15)
                    ' Recupera a parte inteira
                    ImporteEntera = Left(strImporte, 12)
                    ' Recupera a parte decimal
                    ImporteDecimal = Right(strImporte, 2)
                    ' Soma a parte inteira com a parte decimal
                    Importe = CLng(ImporteEntera) + CLng(ImporteDecimal) / 100
                    ' Se importe diferente de 0 e o formato e diferente de vazio
                    If (Importe <> 0 AndAlso EspecieId <> String.Empty) Then
                        objDetalhe = New Detalle
                        objDetalhe.Cantidad = Cantidad
                        objDetalhe.Importe = Importe
                        objDetalhe.Especie.Id = CInt(EspecieId)
                        objDocumento.Detalles.Add(objDetalhe)
                    End If
                Next
            Catch ex As Exception
                ' Atribui falso para a variável
                TodosDetallesRecontadosIdentificados = False
                ' Define a mensagem, espécie não identificada concatenado com o formato
                InformeDatos &= vbCrLf & String.Format(Traduzir("msg_especie_nao_identificada"), IdAPL)
            End Try
        End If
    End Sub

    ''' <summary>
    ''' Método que recupera o número externo é a data de gestão
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [maoliveira] 02/06/2009 Criado
    ''' </history>
    Private Sub EstructurarNumExternoFechaGestion(ByRef objDocumento As Documento)
        ' Variáveis usadas para recuperar os dados
        Dim RECIBOV As String
        Dim NumExterno As String
        Dim FechaGestion As Date

        ' Se a chave existe na lista de valores de entrada
        If (ValorEntrada.Contains(Constantes.AUTOMATA_ARQUIVO_CHAVE_RECIBOV)) Then
            ' Recupera o valor da chave RECIBOV
            RECIBOV = ValorEntrada(Constantes.AUTOMATA_ARQUIVO_CHAVE_RECIBOV)
            ' Recupera o número externo
            NumExterno = Trim(Mid$(RECIBOV, 3, 10))

            ' Procura o NumExterno na lista de campos do formulário
            Dim objCampo = From c In objDocumento.Formulario.Campos _
                           Where c.Nombre = Constantes.AUTOMATA_FORMULARIO_CAMPO_NUMEXTERNO

            ' Se existir
            If (objCampo IsNot Nothing AndAlso objCampo.Count > 0) Then
                ' Atribui ao campo o valor do número externo
                objCampo.First().Valor = NumExterno
            End If

            ' Recupera a data de gestão
            FechaGestion = New Date(Mid$(RECIBOV, 23, 4), Mid$(RECIBOV, 20, 2), Mid$(RECIBOV, 17, 2))
            ' Atribui a data de gestão ao documento
            objDocumento.FechaGestion = FechaGestion
        End If
    End Sub

    ''' <summary>
    ''' Método que recupera o banco, o cliente e a sucursal, quando existir
    ''' </summary>
    ''' <param name="objDocumento">Dados do documento</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [maoliveira] 02/06/2009 Criado
    ''' </history>
    Private Sub EstructurarBancoClienteSucursal(ByRef objDocumento As Documento)
        ' Variáveis usadas para recuperar os dados
        Dim BACLIVA As String
        Dim BancoId As Long, ClienteId As Long
        Dim vIdPsBanco, vIdPsCliente, vIdPsSucursal As String
        Dim IdPsBanco As String = String.Empty
        Dim IdPsCliente As String = String.Empty
        Dim IdPsClienteTemp As String = String.Empty
        Dim IdPsSucursal As String = String.Empty
        Const VALOR_SUCURSAL As String = "-000"

        ' Se a chave existe na lista de valores de entrada
        If (ValorEntrada.Contains(Constantes.AUTOMATA_ARQUIVO_CHAVE_BACLIVA)) Then

            ' Recupera o valor da chave BACLIVA
            BACLIVA = ValorEntrada(Constantes.AUTOMATA_ARQUIVO_CHAVE_BACLIVA)

            ' Recupera o IdPs do banco
            vIdPsBanco = Trim(Mid(BACLIVA, 1, 5))
            ' Recupera o IdPs do cliente
            vIdPsCliente = Trim(Mid(BACLIVA, 7, 5))
            ' Recupera o IdPs do Sucursal
            vIdPsSucursal = Trim(Mid(BACLIVA, 13, 5))

            ' Se é numérico
            If IsNumeric(vIdPsBanco) Then
                ' Atribui o IdPs recuperado do banco a variável
                IdPsBanco = CLng(vIdPsBanco)
            End If

            ' Se é numérico
            If IsNumeric(vIdPsCliente) Then
                ' Atribui o IdPs recuperado do cliente a variável
                IdPsCliente = CLng(vIdPsCliente)
            End If

            ' Se é numérico
            If IsNumeric(vIdPsSucursal) Then
                ' Atribui o IdPs recuperado da sucursal a variável
                IdPsSucursal = CLng(vIdPsSucursal)
            End If

            ' Recupera o código do banco de acordo com o IdPs passado como parâmetro
            BancoId = EntidadId(Enumeradores.Automata_Entidade.Banco, IdPsBanco)

            ' Procura o campo Banco na lista de campos do formulário
            Dim objCampoB = From c In objDocumento.Formulario.Campos _
                         Where c.Nombre.Contains(Constantes.AUTOMATA_FORMULARIO_CAMPO_BANCO)

            ' Se existir
            If (objCampoB IsNot Nothing AndAlso objCampoB.Count > 0) Then
                ' Atribui o identificador do banco ao campo do formulário
                objCampoB.First().IdValor = BancoId
            End If

            ' Se o código do banco não foi encontrado
            If (BancoId = 0) Then
                If EnviaCorreoExitoso Then
                    ' Define uma mensagem, banco não encontrado
                    InformeDatos &= vbCrLf & Traduzir("msg_banco_nao_encontrato")
                End If
            End If

            ' Recupera o IdPs da Sucursal
            IdPsSucursal = Right(IdPsSucursal.ToString.PadLeft(3, Constantes.AUTOMATA_ARQUIVO_COMPLENTAR_NUMERO), 3)

            ' Concatena o IdPs do cliente com o IdPs da sucursal
            IdPsClienteTemp = IdPsCliente + "-" + IdPsSucursal

            ' Recupera o código do cliente, de acordo com o IdPs passado como parâmetro
            ClienteId = EntidadId(Enumeradores.Automata_Entidade.Cliente, IdPsClienteTemp)

            ' Se o identificador do cliente não foi encontrado
            If ClienteId = 0 Then

                ' Recupera o código do cliente, de acordo com o IdPs passado como parâmetro
                ClienteId = EntidadId(Enumeradores.Automata_Entidade.Cliente, IdPsCliente)

                ' Se o identificador do cliente não foi encontrado
                If ClienteId = 0 Then
                    If EnviaCorreoExitoso Then
                        ' Define uma mensagem, sucursal não encontrado
                        InformeDatos &= vbCrLf & Traduzir("msg_sucursal_nao_encontrado")
                    End If
                    ' Recupera o IdPs do cliente
                    IdPsCliente = If(Len(IdPsCliente) > 4, Left(IdPsCliente, Len(IdPsCliente) - 4), IdPsCliente) & VALOR_SUCURSAL
                    ' Recupera o código do cliente, de acordo com o IdPs passado como parâmetro
                    ClienteId = EntidadId(Enumeradores.Automata_Entidade.Cliente, IdPsCliente)
                End If
            End If

            ' Procura o campo ClienteOrigen na lista de campos do formulário
            Dim objCampoCO = From c In objDocumento.Formulario.Campos _
                            Where c.Nombre.Contains(Constantes.AUTOMATA_FORMULARIO_CAMPO_CLIENTEORIGEN)

            ' Se existir
            If (objCampoCO IsNot Nothing AndAlso objCampoCO.Count > 0) Then
                ' Atribui o identificador do cliente ao campo do formulário
                objCampoCO.First().IdValor = ClienteId
            End If

            ' Se o código do cliente não foi encontrado
            If (ClienteId = 0) Then
                If EnviaCorreoExitoso Then
                    ' Define uma mensagem, cliente não encontrado
                    InformeDatos &= vbCrLf & Traduzir("msg_cliente_nao_encontrato")
                End If
            End If

        End If

    End Sub

    ''' <summary>
    ''' Método que recupera o banco, o cliente e a sucursal, quando existir do destino
    ''' </summary>
    ''' <param name="objDocumento">Dados do documento</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [maoliveira] 02/06/2009 Criado
    ''' </history>
    Private Sub EstructurarBancoClienteSucursalDestino(ByRef objDocumento As Documento)
        ' Variáveis usadas para recuperar os dados
        Dim BACLIDE As String
        Dim BancoId As Long, ClienteId As Long
        Dim vIdPsBanco, vIdPsCliente, vIdPsSucursal As String
        Dim IdPsBanco As String = String.Empty
        Dim IdPsCliente As String = String.Empty
        Dim IdPsClienteTemp As String = String.Empty
        Dim IdPsSucursal As String = String.Empty
        Const VALOR_SUCURSAL As String = "-000"

        ' Se a chave existe na lista de valores de entrada
        If (ValorEntrada.Contains(Constantes.AUTOMATA_ARQUIVO_CHAVE_BACLIDE) OrElse ValorEntrada.Contains(Constantes.AUTOMATA_ARQUIVO_CHAVE_BACLIVA)) Then

            ' Verifica se a chave BACLIDE existe
            If (ValorEntrada.Contains(Constantes.AUTOMATA_ARQUIVO_CHAVE_BACLIDE)) Then
                ' Recupera o valor da chave BACLIDE
                BACLIDE = ValorEntrada(Constantes.AUTOMATA_ARQUIVO_CHAVE_BACLIDE)
            Else
                ' Recupera o valor da chave BACLIVA
                BACLIDE = ValorEntrada(Constantes.AUTOMATA_ARQUIVO_CHAVE_BACLIVA)
            End If

            ' Recupera o IdPs do banco
            vIdPsBanco = Trim(Mid(BACLIDE, 1, 5))
            ' Recupera o IdPs do cliente
            vIdPsCliente = Trim(Mid(BACLIDE, 7, 5))
            ' Recupera o IdPs do Sucursal
            vIdPsSucursal = Trim(Mid(BACLIDE, 13, 5))

            ' Se é numérico
            If IsNumeric(vIdPsBanco) Then
                ' Atribui o IdPs recuperado do banco a variável
                IdPsBanco = CLng(vIdPsBanco)
            End If

            ' Se é numérico
            If IsNumeric(vIdPsCliente) Then
                ' Atribui o IdPs recuperado do banco a variável
                IdPsCliente = CLng(vIdPsCliente)
            End If

            ' Se é numérico
            If IsNumeric(vIdPsSucursal) Then
                ' Atribui o IdPs recuperado do banco a variável
                IdPsSucursal = CLng(vIdPsSucursal)
            End If

            ' Recupera o IdPs da Sucursal
            IdPsSucursal = Right(IdPsSucursal.PadLeft(3, Constantes.AUTOMATA_ARQUIVO_COMPLENTAR_NUMERO), 3)
            ' Recupera o código do banco de acordo com o IdPs passado como parâmetro
            BancoId = EntidadId(Enumeradores.Automata_Entidade.Banco, IdPsBanco)

            ' Procura o campo Banco Deposito na lista de campos do formulário
            Dim objCampoBD = From c In objDocumento.Formulario.Campos _
                         Where c.Nombre.Contains(Constantes.AUTOMATA_FORMULARIO_CAMPO_BANCODEPOSITO)

            ' Se existir
            If (objCampoBD IsNot Nothing AndAlso objCampoBD.Count > 0) Then
                ' Atribui o identificador do banco ao campo do formulário
                objCampoBD.First().IdValor = BancoId
            End If

            ' Se código do banco não existir
            If (BancoId = 0) Then
                If EnviaCorreoExitoso Then
                    ' Define uma mensagem, banco não encontrado
                    InformeDatos &= vbCrLf & Traduzir("msg_banco_nao_encontrato")
                End If
            End If

            ' Recupera o IdPs da Sucursal
            IdPsSucursal = Right(IdPsSucursal.ToString.PadLeft(3, Constantes.AUTOMATA_ARQUIVO_COMPLENTAR_NUMERO), 3)

            ' Concatena o IdPs do cliente com o IdPs da sucursal
            IdPsClienteTemp = IdPsCliente & "-" & IdPsSucursal

            ' Recupera o código do cliente, de acordo com o IdPs passado como parâmetro
            ClienteId = EntidadId(Enumeradores.Automata_Entidade.Cliente, IdPsClienteTemp)

            ' Se o identificador do cliente não foi encontrado
            If ClienteId = 0 Then

                ' Recupera o código do cliente, de acordo com o IdPs passado como parâmetro
                ClienteId = EntidadId(Enumeradores.Automata_Entidade.Cliente, IdPsCliente)

                ' Se o identificador do cliente não foi encontrado
                If ClienteId = 0 Then
                    If EnviaCorreoExitoso Then
                        ' Define uma mensagem, sucursal não encontrado
                        InformeDatos &= vbCrLf & Traduzir("msg_sucursal_destino_nao_encontrado")
                    End If
                    ' Recupera o IdPs do cliente
                    IdPsCliente = Left(IdPsCliente, Len(IdPsCliente) - 4) & VALOR_SUCURSAL
                    ' Recupera o código do cliente, de acordo com o IdPs passado como parâmetro
                    ClienteId = EntidadId(Enumeradores.Automata_Entidade.Cliente, IdPsCliente)
                End If
            End If

            ' Procura o campo cliente destino na lista de campos do formulário
            Dim objCampoCD = From c In objDocumento.Formulario.Campos _
                            Where c.Nombre.Contains(Constantes.AUTOMATA_FORMULARIO_CAMPO_CLIENTEDESTINO)

            ' Se existir
            If (objCampoCD IsNot Nothing AndAlso objCampoCD.Count > 0) Then
                ' Atribui o identificador do cliente ao campo do formulário
                objCampoCD.First().IdValor = ClienteId
            End If

            ' Se não encontrou o código do cliente
            If (ClienteId = 0) Then
                If EnviaCorreoExitoso Then
                    ' Define uma mensagem, cliente não encontrado
                    InformeDatos &= vbCrLf & Traduzir("msg_cliente_destino_nao_encontrato")
                End If
            End If

        End If

    End Sub

    ''' <summary>
    ''' Método que recupera o código do documento
    ''' </summary>
    ''' <param name="objDocumento">Dados do documento</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [maoliveira] 02/06/2009 Criado
    ''' </history>
    Private Sub EstructurarIdOrigen(ByRef objDocumento As Documento)
        ' Variáveis usadas para recuperar os dados
        Dim IDDOCET As String, DocID As String

        ' Se a chave existe na lista de valores de entrada
        If (ValorEntrada.Contains(Constantes.AUTOMATA_ARQUIVO_CHAVE_IDDOCET)) Then
            ' Recupera o valor da chave IDDOCET
            IDDOCET = ValorEntrada(Constantes.AUTOMATA_ARQUIVO_CHAVE_IDDOCET)
            ' Recupera o código do documento
            DocID = Trim(Mid$(IDDOCET, 1, 12))
            ' Passa o código do documento de origem para o documento
            objDocumento.Origen.Id = IIf(DocID <> vbNullString, DocID, 0)
        End If
    End Sub

    ''' <summary>
    ''' Método que recupera os dados do documento 
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [maoliveira] 02/06/2009 Criado
    ''' </history>
    Private Sub EstructurarIdOrigenDesdeNumExterno(ByRef objDocumento As Documento)
        ' Variáveis usadas para recuperar os dados
        Dim IdOrigen, CentroProcesoId As Long
        Dim IdDocDetalles, IdDocCamposExtra, IdDocBultos, IdPrimordial As Integer
        Dim NumExterno As String = String.Empty

        ' Procura o 'NumExterno' na lista de campos do formulário
        Dim objCampo = From c In objDocumento.Formulario.Campos _
                       Where c.Nombre.Contains(Constantes.AUTOMATA_FORMULARIO_CAMPO_NUMEXTERNO)

        ' Se existir
        If (objCampo IsNot Nothing AndAlso objCampo.Count > 0) Then
            ' Atribui ao campo o valor do número externo
            NumExterno = objCampo.First().Valor
        End If

        ' Procura o 'CentroProcesoOrigen' na lista de campos do formulário
        Dim objCampoCPO = From c In objDocumento.Formulario.Campos _
                       Where c.Nombre.Contains(Constantes.AUTOMATA_FORMULARIO_CAMPO_CENTROPROCESOORIGEN)

        ' Se existir
        If (objCampoCPO IsNot Nothing AndAlso objCampoCPO.Count > 0) Then
            ' Atribui ao campo o valor do número externo
            CentroProcesoId = objCampoCPO.First().IdValor
        End If

        ' Define a conexão a ser usada
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        ' Define o script a ser executado
        comando.CommandText = My.Resources.IdOrigenDesdeNumExterno.ToString()
        ' Define o tipo do script
        comando.CommandType = CommandType.Text

        ' Define os parametros
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, Constantes.AUTOMATA_FORMULARIO_CAMPO_NUMEXTERNO, ProsegurDbType.Descricao_Longa, NumExterno))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdCentroProceso", ProsegurDbType.Inteiro_Curto, CentroProcesoId))

        ' Recupera os documento
        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)

        ' Se Existe
        If (dt.Rows.Count > 0) Then
            Dim dr As DataRow = dt.Rows(0)
            ' Recupera os dados
            If (Not IsDBNull(dr("IdOrigen"))) Then
                IdOrigen = dr("IdOrigen")
            Else
                IdOrigen = 0
            End If
            If (Not IsDBNull(dr("IdDocDetalles"))) Then
                IdDocDetalles = dr("IdDocDetalles")
            Else
                IdDocDetalles = 0
            End If
            If (Not IsDBNull(dr("IdDocCamposExtra"))) Then
                IdDocCamposExtra = dr("IdDocCamposExtra")
            Else
                IdDocCamposExtra = 0
            End If
            If (Not IsDBNull(dr("IdDocBultos"))) Then
                IdDocBultos = dr("IdDocBultos")
            Else
                IdDocBultos = 0
            End If
            If (Not IsDBNull(dr("IdPrimordial"))) Then
                IdPrimordial = dr("IdPrimordial")
            Else
                IdPrimordial = 0
            End If
        End If

        ' Passa os dados para o objeto Documento
        If (objDocumento.Origen Is Nothing) Then objDocumento.Origen = New Documento()
        objDocumento.Origen.Id = IdOrigen
        objDocumento.Detalles.Documento.Id = IdDocDetalles
        objDocumento.Formulario.CamposExtra.Documento.Id = IdDocCamposExtra
        objDocumento.Bultos.Documento.Id = IdDocBultos
        objDocumento.Primordial.Id = IdPrimordial

        ' Se não existir
        If IdOrigen = 0 Then
            If EnviaCorreoExitoso Then
                ' Define uma mensagem código da origem não encontrado
                InformeDatos &= vbCrLf & Traduzir("msg_documento_origem_nao_encontrato")
            End If
        End If
    End Sub

    ''' <summary>
    ''' Método que recuperao centro de processo
    ''' </summary>
    ''' <param name="objDocumento">Dados do documento</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [maoliveira] 02/06/2009 Criado
    ''' </history>
    Private Sub EstructurarCentroProceso(ByRef objDocumento As Documento)
        ' Variáveis usadas para recuperar os dados
        Dim PLANSUC As String, CNTRPRC As String
        Dim IdPsPlanta As String, IdPsTCP As String, IdPsCP As String
        Dim CentroProcesoId As Long

        ' Se as chaves existem na lista de valores de entrada
        If (ValorEntrada.Contains(Constantes.AUTOMATA_ARQUIVO_CHAVE_PLANSUC) AndAlso ValorEntrada.Contains(Constantes.AUTOMATA_ARQUIVO_CHAVE_CNTRPRC)) Then

            ' Recupera o valor da chave PLANSUC
            PLANSUC = ValorEntrada(Constantes.AUTOMATA_ARQUIVO_CHAVE_PLANSUC)
            ' Recupera o valor da chave CNTRPRC
            CNTRPRC = ValorEntrada(Constantes.AUTOMATA_ARQUIVO_CHAVE_CNTRPRC)

            ' Recupera o código da planta
            IdPsPlanta = Mid$(PLANSUC, 1, 3)
            ' Recupera o código do tipo de centro de processo
            IdPsTCP = Mid$(CNTRPRC, 1, 3)
            ' Recupera o código do centro de processo
            IdPsCP = IdPsPlanta & IdPsTCP

            ' Recupera o código do centro de processo
            CentroProcesoId = EntidadId(Enumeradores.Automata_Entidade.CentroProcesso, IdPsCP)

            ' Procura o campo Enumeradores.Automata_Formulario_Campo.CentroProcesoOrigen.ToString na lista de campos do formulário
            Dim objCampoCPO = From c In objDocumento.Formulario.Campos _
                            Where c.Nombre.Contains(Constantes.AUTOMATA_FORMULARIO_CAMPO_CENTROPROCESOORIGEN)

            ' Se existir
            If (objCampoCPO IsNot Nothing AndAlso objCampoCPO.Count > 0) Then
                ' Atribui o identificador do cliente ao campo do formulário
                objCampoCPO.First().IdValor = CentroProcesoId
            End If

            ' Se não encontrou o código do centro de processo
            If (CentroProcesoId = 0) Then
                If EnviaCorreoExitoso Then
                    ' Define uma mensagem de centro de processo de origem não encontrado
                    InformeDatos &= vbCrLf & Traduzir("msg_centroprocesso_nao_encontrato")
                End If
            End If

        End If
    End Sub

    ''' <summary>
    ''' Método que recupera o centro de processo destino
    ''' </summary>
    ''' <param name="objDocumento">Dados do documento</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [maoliveira] 02/06/2009 Criado
    ''' </history>
    Private Sub EstructurarCentroProcesoDestino(ByRef objDocumento As Documento)
        ' Variáveis usadas para recuperar os dados
        Dim PLANSUC As String, CNTDEST As String
        Dim IdPsPlanta As String, IdPsTCP As String, IdPsCP As String
        Dim CentroProcesoId As Long

        ' Se as chaves existem na lista de valores de entrada
        If (ValorEntrada.Contains(Constantes.AUTOMATA_ARQUIVO_CHAVE_PLANSUC) AndAlso ValorEntrada.Contains(Constantes.AUTOMATA_ARQUIVO_CHAVE_CNTDEST)) Then

            ' Recupera o valor da chave PLANSUC
            PLANSUC = ValorEntrada(Constantes.AUTOMATA_ARQUIVO_CHAVE_PLANSUC)
            ' Recupera o valor da chave CNTDEST
            CNTDEST = ValorEntrada(Constantes.AUTOMATA_ARQUIVO_CHAVE_CNTDEST)

            ' Recupera o código da planta
            IdPsPlanta = Mid$(PLANSUC, 1, 3)
            ' Recupera o código do tipo de centro de processo
            IdPsTCP = Mid$(CNTDEST, 1, 3)
            ' Recupera o código do centro de processo
            IdPsCP = IdPsPlanta & IdPsTCP

            ' Recupera o código do centro de processo
            CentroProcesoId = EntidadId(Enumeradores.Automata_Entidade.CentroProcesso, IdPsCP)

            ' Procura o campo Enumeradores.Automata_Formulario_Campo.CentroProcesoDestino.ToString na lista de campos do formulário
            Dim objCampoCPD = From c In objDocumento.Formulario.Campos _
                            Where c.Nombre.Contains(Constantes.AUTOMATA_FORMULARIO_CAMPO_CENTROPROCESODESTINO)

            ' Se existir
            If (objCampoCPD IsNot Nothing AndAlso objCampoCPD.Count > 0) Then
                ' Atribui o identificador do cliente ao campo do formulário
                objCampoCPD.First().IdValor = CentroProcesoId
            End If

            ' Se não encontrou o código do centro de processo
            If (CentroProcesoId = 0) Then
                If EnviaCorreoExitoso Then
                    ' Define uma mensagem de centro de processo de origem não encontrado
                    InformeDatos &= vbCrLf & Traduzir("msg_centroprocesso_destino_nao_encontrato")
                End If
            End If

        End If
    End Sub

    ''' <summary>
    ''' Método que recuperá o codigo da entidade passada como parametro
    ''' </summary>
    ''' <param name="entidad">Nome da Entidade</param>
    ''' <param name="IdPs">Identificador da Entidade</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [maoliveira] 02/06/2009 Criado
    ''' </history>
    Private Function EntidadId(entidad As Enumeradores.Automata_Entidade, IdPs As String) As Long
        ' Inicializa a entidade com 0 (valor padrão)
        EntidadId = 0

        ' Verifica o tipo de entidade
        Select Case entidad
            Case Enumeradores.Automata_Entidade.Banco
                ' Recupera o código do banco
                Dim lstBanco As New Negocio.Bancos
                lstBanco.IdPS = IdPs
                lstBanco.Realizar()
                ' Se o banco existir
                If (lstBanco.Count > 0) Then
                    EntidadId = lstBanco.First().Id
                End If
            Case Enumeradores.Automata_Entidade.Cliente
                ' Recupera o código do cliente
                Dim lstCliente As New Negocio.Clientes
                lstCliente.IdPS = IdPs
                lstCliente.Realizar()
                ' Se o cliente existir
                If (lstCliente.Count > 0) Then
                    EntidadId = lstCliente.First().Id
                End If
            Case Enumeradores.Automata_Entidade.CentroProcesso
                ' Recupera o código do centro de processo
                Dim lstCentroProcesso As New Negocio.CentrosProceso
                lstCentroProcesso.IdPS = IdPs
                lstCentroProcesso.Realizar()
                ' Se o centro de processo existir
                If (lstCentroProcesso.Count > 0) Then
                    EntidadId = lstCentroProcesso.First().Id
                End If
        End Select

        ' Retorna o código da Entidade
        Return EntidadId

    End Function

    ''' <summary>
    ''' Este método tem como objetivo recuperar do banco de dados todas as informações necessárias 
    ''' para a configuração do documento que será exportado
    ''' </summary>
    ''' <param name="Doc"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [maoliveira] 01/06/2009 Criado
    ''' </history>
    Private Sub LeerDocumento(Doc As Documento)

        ' Recupera as informações do documento
        Doc.Realizar()

        Negocio.Util.LogMensagemEmDisco("        - Leendo documento: " & Doc.Id.ToString())

        ' Se existir campos
        If (Doc.Formulario.Campos.Count > 0) Then

            ' Verifica se existem o campo 'CentroProcesoDestino'
            Dim objCampoCPD = From c In Doc.Formulario.Campos _
                       Where c.Nombre.Contains(Constantes.AUTOMATA_FORMULARIO_CAMPO_CENTROPROCESODESTINO)

            ' Se Existir
            If objCampoCPD IsNot Nothing AndAlso objCampoCPD.Count > 0 Then

                ' Recupera os dados
                Dim objCentroProcesso As New CentroProceso()
                objCentroProcesso.Id = objCampoCPD(0).IdValor
                objCentroProcesso.Realizar()

                ' Adiciona ao elemento do formulário
                objCampoCPD(0).Elemento = New Elemento()
                objCampoCPD(0).Elemento.IdPS = objCentroProcesso.IdPS
                objCampoCPD(0).Elemento.Descripcion = objCentroProcesso.Descripcion
                objCampoCPD(0).Elemento.Planta = objCentroProcesso.Planta

            Else
                ' Verifica se existem o campo 'CentroProcesoOrigen'
                Dim objCampoCPO = From c In Doc.Formulario.Campos _
                           Where c.Nombre.Contains(Constantes.AUTOMATA_FORMULARIO_CAMPO_CENTROPROCESOORIGEN)

                ' Se Existir
                If objCampoCPO IsNot Nothing AndAlso objCampoCPO.Count > 0 Then

                    ' Recupera os dados
                    Dim objCentroProcesso As New CentroProceso()
                    objCentroProcesso.Id = objCampoCPO(0).IdValor
                    objCentroProcesso.Realizar()

                    ' Adiciona ao elemento do formulário
                    objCampoCPO(0).Elemento = New Elemento()
                    objCampoCPO(0).Elemento.IdPS = objCentroProcesso.IdPS
                    objCampoCPO(0).Elemento.Descripcion = objCentroProcesso.Descripcion
                    objCampoCPO(0).Elemento.Planta = objCentroProcesso.Planta

                End If
            End If

            If Not Doc.EsGrupo Then

                ' Verifica se existem o campo 'Banco'
                Dim objCampoB = From c In Doc.Formulario.Campos _
                               Where c.Nombre.Contains(Constantes.AUTOMATA_FORMULARIO_CAMPO_BANCO)

                ' Se Existir
                If objCampoB IsNot Nothing AndAlso objCampoB.Count > 0 Then

                    ' Recupera os dados
                    Dim objBanco As New Banco()
                    objBanco.Id = objCampoB(0).IdValor
                    objBanco.Realizar()

                    ' Adiciona ao elemento do formulário
                    objCampoB(0).Elemento = New Elemento()
                    objCampoB(0).Elemento.IdPS = objBanco.IdPS
                    objCampoB(0).Elemento.Descripcion = objBanco.Descripcion

                End If

                ' Verifica se existem o campo 'BancoDeposito'
                Dim objCampoBD = From c In Doc.Formulario.Campos _
                            Where c.Nombre.Contains(Constantes.AUTOMATA_FORMULARIO_CAMPO_BANCODEPOSITO)

                ' Se existir
                If objCampoBD IsNot Nothing AndAlso objCampoBD.Count > 0 Then

                    ' Recupera os dados
                    Dim objBanco As New Banco()
                    objBanco.Id = objCampoBD(0).IdValor
                    objBanco.Realizar()

                    ' Adiciona ao elemento do formulário
                    objCampoBD(0).Elemento = New Elemento()
                    objCampoBD(0).Elemento.IdPS = objBanco.IdPS
                    objCampoBD(0).Elemento.Descripcion = objBanco.Descripcion

                End If

                ' Verifica se existem o campo 'ClienteOrigen'
                Dim objCampoCO = From c In Doc.Formulario.Campos _
                          Where c.Nombre.Contains(Constantes.AUTOMATA_FORMULARIO_CAMPO_CLIENTEORIGEN)

                ' Se existir
                If objCampoCO IsNot Nothing AndAlso objCampoCO.Count > 0 Then

                    ' Recupera os dados
                    Dim objCliente As New Cliente()
                    objCliente.Id = objCampoCO(0).IdValor
                    objCliente.Realizar()

                    ' Adiciona ao elemento do formulário
                    objCampoCO(0).Elemento = New Elemento()
                    objCampoCO(0).Elemento.IdPS = objCliente.IdPS
                    objCampoCO(0).Elemento.Descripcion = objCliente.Descripcion

                End If

                ' Verifica se existem o campo 'ClienteDestino'
                Dim objCampoCD = From c In Doc.Formulario.Campos _
                          Where c.Nombre.Contains(Constantes.AUTOMATA_FORMULARIO_CAMPO_CLIENTEDESTINO)

                ' Se existir
                If objCampoCD IsNot Nothing AndAlso objCampoCD.Count > 0 Then

                    ' Recupera os dados
                    Dim objCliente As New Cliente()
                    objCliente.Id = objCampoCD(0).IdValor
                    objCliente.Realizar()

                    ' Adiciona ao elemento do formulário
                    objCampoCD(0).Elemento = New Elemento()
                    objCampoCD(0).Elemento.IdPS = objCliente.IdPS
                    objCampoCD(0).Elemento.Descripcion = objCliente.Descripcion

                End If
            End If

        End If

        ' Recupera as informações dos campos extras
        Doc.Formulario.CamposExtra.Realizar()

        ' Se existir pacotes
        If Doc.Formulario.ConBultos Then
            ' Recupera as informações dos pacotes
            Doc.Bultos.Realizar()
        End If

        ' Se existir valores
        If Doc.Formulario.ConValores Then
            ' Recupera as informações dos valores
            Doc.Detalles.Realizar()
        End If

    End Sub

    ''' <summary>
    ''' Método que escreve os dados no arquivo
    ''' </summary>
    ''' <param name="objDocumento"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [maoliveira] 01/06/2009 Criado
    ''' </history>
    Private Sub EscribirArchivoExportado(objDocumento As Documento)

        Try
            Negocio.Util.LogMensagemEmDisco("        - Gravando documento: " & objDocumento.Id.ToString())
            ' Abre o arquivo
            AbrirArchivoExportado(objDocumento.Id)
            ' Grava o código do documento
            EscribirId(objDocumento.Id)
            ' Grava a data de criação do documento
            EscribirFecha(objDocumento.Fecha)
            ' Grava o formato de exportação do arquivo
            EscribirFormatoExportacion()
            ' Grava o número externo e a data de gestão
            EscribirNumExternoFechaGestion(objDocumento.Formulario.Campos, objDocumento.FechaGestion)
            ' Grava os campos do formulário
            EscribirCampos(objDocumento.Formulario.Campos, objDocumento.Formulario.CamposExtra)
            ' Grava os camps extras do formulário
            EscribirCamposExtra(objDocumento.Formulario.CamposExtra)
            ' Grava os pacotes do documento
            EscribirBultos(objDocumento.Bultos)
            ' Grava os detalhes do documento
            EscribirDetalles(objDocumento.Detalles)
            ' Fecha o arquivo
            CerrarArchivoExportado()
        Catch ex As Exception
            Throw
        End Try
        

    End Sub

    ''' <summary>
    ''' Método que escreve o código do documento
    ''' </summary>
    ''' <param name="id">código do documento</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [maoliveira] 01/06/2009 Criado
    ''' </history>
    Private Sub EscribirId(id As String)
        ' Escreve o código do documento no arquivo, a palavra chave IDDOCET é usada para identificação posterior
        ArchivoExportado.WriteLine(Constantes.AUTOMATA_ARQUIVO_LINHA_VALOR_INICIAL & _
                                   Constantes.AUTOMATA_ARQUIVO_SEPARADOR & _
                                   Constantes.AUTOMATA_ARQUIVO_CHAVE_IDDOCET & _
                                   Constantes.AUTOMATA_ARQUIVO_SEPARADOR & id)
    End Sub

    ''' <summary>
    ''' Método que escreve os dados dos detalhes do documento no arquivo
    ''' </summary>
    ''' <param name="lstDetalhes">Lista de Detalhes</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [maoliveira] 01/06/2009 Criado
    ''' </history>
    Private Sub EscribirDetalles(lstDetalhes As Detalles)

        ' Se existe
        If lstDetalhes.Count > 0 Then
            Dim IdAPLIC As String

            ' Para cada detalhe
            For Each detalhe As Detalle In lstDetalhes
                ' Verifica o formato de exportação do arquivo
                If (Me.FormatoExportacion = Constantes.AUTOMATA_ARQUIVO_VERSAO_SIGII) Then
                    If IdSIGII_EspecieId.Contains(CStr(detalhe.Especie.Id)) Then
                        IdAPLIC = Right(IdSIGII_EspecieId(CStr(detalhe.Especie.Id)).ToString().PadLeft(7, Constantes.AUTOMATA_ARQUIVO_SEPARADOR), 7)
                    Else
                        Throw New Exception(String.Format(Traduzir("msg_especie_nao_configurada"), detalhe.Especie.Descripcion, detalhe.Especie.Moneda.Descripcion))
                    End If
                Else
                    If IdRBO_EspecieId.Contains(CStr(detalhe.Especie.Id)) Then
                        IdAPLIC = Right(IdRBO_EspecieId(CStr(detalhe.Especie.Id)).ToString().PadLeft(7, Constantes.AUTOMATA_ARQUIVO_SEPARADOR), 7)
                    Else
                        Throw New Exception(String.Format(Traduzir("msg_especie_nao_configurada"), detalhe.Especie.Descripcion, detalhe.Especie.Moneda.Descripcion))
                    End If
                End If

                ' Recupera os valores 
                Dim Cantidad As String = Right(detalhe.Cantidad.ToString().PadLeft(14, Constantes.AUTOMATA_ARQUIVO_COMPLENTAR_NUMERO), 14)
                Dim Importe As String = Right(FormatNumber(detalhe.Importe, 2, vbTrue, vbFalse, vbFalse).PadLeft(17, Constantes.AUTOMATA_ARQUIVO_COMPLENTAR_NUMERO), 17)
                Dim ImporteEntera As String = Left(Importe, 14)
                Dim ImporteDecimal As String = Right(Importe, 2)

                ' Escreve os dados no arquivo, a palavra chave MONTOXX é usada para identificação posterior
                ArchivoExportado.WriteLine(Constantes.AUTOMATA_ARQUIVO_LINHA_VALOR_INICIAL & _
                                   Constantes.AUTOMATA_ARQUIVO_SEPARADOR & _
                                   Constantes.AUTOMATA_ARQUIVO_CHAVE_MONTOXX & _
                                   Constantes.AUTOMATA_ARQUIVO_SEPARADOR & IdAPLIC & _
                                   Constantes.AUTOMATA_ARQUIVO_SEPARADOR & Cantidad & _
                                   Constantes.AUTOMATA_ARQUIVO_SEPARADOR & ImporteEntera & "," & ImporteDecimal)
            Next
        End If
    End Sub

    ''' <summary>
    ''' Método que escreve os dados dos pacotes do documento no arquivo
    ''' </summary>
    ''' <param name="lstBultos">Lista com os pacotes</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [maoliveira] 01/06/2009 Criado
    ''' </history>
    Private Sub EscribirBultos(lstBultos As Bultos)
        ' Se existe
        If (lstBultos.Count > 0) Then
            ' Inicializa a variavel para verificação se o destino do pacote já foi informado
            Dim conDestino As Boolean = False

            ' Para cada pacate 
            For Each bulto As Bulto In lstBultos
                ' Se não gravou o destino
                If (Not conDestino) Then
                    conDestino = True
                    ' Escreve o destino no arquivo, a palavra chave DESTINO é usada para identificação posterior
                    ArchivoExportado.WriteLine(Constantes.AUTOMATA_ARQUIVO_LINHA_VALOR_INICIAL & _
                                   Constantes.AUTOMATA_ARQUIVO_SEPARADOR & _
                                   Constantes.AUTOMATA_ARQUIVO_CHAVE_DESTINO & _
                                   Constantes.AUTOMATA_ARQUIVO_SEPARADOR & bulto.Destino.Id)
                End If

                ' Escreve o código da bolsa e o número do precinto, a palavra chave BOLPREC é usada para identificação posterior
                ArchivoExportado.WriteLine(Constantes.AUTOMATA_ARQUIVO_LINHA_VALOR_INICIAL & _
                                   Constantes.AUTOMATA_ARQUIVO_SEPARADOR & _
                                   Constantes.AUTOMATA_ARQUIVO_CHAVE_BOLPREC & _
                                   Constantes.AUTOMATA_ARQUIVO_SEPARADOR & _
                                   Right(bulto.CodBolsa.PadLeft(10, Constantes.AUTOMATA_ARQUIVO_SEPARADOR), 10) & _
                                   Constantes.AUTOMATA_ARQUIVO_SEPARADOR & _
                                   Right(bulto.NumPrecinto.PadLeft(10, Constantes.AUTOMATA_ARQUIVO_SEPARADOR), 10))
            Next
        End If
    End Sub

    ''' <summary>
    ''' Método que escreve no arquivo os campos extras do formulário
    ''' </summary>
    ''' <param name="lstCamposExtras">Lista com os campos extras</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [maoliveira] 01/06/2009 Criado
    ''' </history>
    Private Sub EscribirCamposExtra(lstCamposExtras As CamposExtra)

        ' Declara a variável usada na interação
        Dim item

        ' Para cada item do dicionário
        For Each item In Diccionario

            ' Verifica se a chave do dicionário está presente na lista dos campos extras
            Dim objCamposExtras = From ce In lstCamposExtras _
                                  Where ce.Nombre = item.Value

            If (objCamposExtras IsNot Nothing AndAlso objCamposExtras.Count > 0) Then
                ' Escreve o campo extra no arquivo, a palavra chave "item.Key" é usada para identificação posterior
                ArchivoExportado.WriteLine(Constantes.AUTOMATA_ARQUIVO_LINHA_VALOR_INICIAL & _
                                   Constantes.AUTOMATA_ARQUIVO_SEPARADOR & item.Key & _
                                   Constantes.AUTOMATA_ARQUIVO_SEPARADOR & objCamposExtras(0).Valor)
            End If
        Next
    End Sub

    ''' <summary>
    ''' Método que fecha os arquivos que foram exportados
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [maoliveira] 01/06/2009 Criado
    ''' </history>
    Private Sub CerrarArchivoExportado()
        ' Fecha o arquivo que foi exportado
        ArchivoExportado.Close()
        ' Limpa o arquivo da memoria
        ArchivoExportado.Dispose()
    End Sub

    ''' <summary>
    ''' Método que escreve no arquivo os dados dos campos do formulário
    ''' </summary>
    ''' <param name="lstCampos">Lista com os campos do formulário</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [maoliveira] 01/06/2009 Criado
    ''' [vinicius.gama] 20/01/2010 Alterado - Se a exportacao do documento for para o RBO e foi informado o campo extra Bacliva, 
    ''' obtem os dados do campo extra
    ''' </history>
    Private Sub EscribirCampos(lstCampos As Campos, lstCamposExtra As CamposExtra)

        ' Variavel para guardar o valor do campo extra bacliva
        Dim BaclivaValor As String = String.Empty
        ' Variavel que define o id do campo extra bacliva no banco de dados
        Dim IdCampoExtraBacliva As Integer = CInt(Prosegur.Genesis.Web.Login.Configuraciones.AppSettings("IdCampoExtraBacliva"))
        ' Lambda que busca o campo extra que representa o bacliva
        Dim CampoExtraBacliva As CampoExtra = lstCamposExtra.Find(Function(CEx As CampoExtra) CEx.Id = IdCampoExtraBacliva)

        ' Se encontrou o campo extra define o valor do campo bacliva
        If CampoExtraBacliva IsNot Nothing Then
            BaclivaValor = CampoExtraBacliva.Valor
        End If

        ' Se existe o campo extra BACLIVA e seu valor nao for vazio busca as informacoes dele, senao busca a partir dos campos do documento.
        ' Escreve no arquivo, os dados do banco, do cliente e da sucursal.
        If BaclivaValor <> String.Empty Then
            EscribirBacliva(BaclivaValor)
        Else
            EscribirBancoClienteSucursal(lstCampos)
        End If

        ' Escreve no arquivo, os dados do centro de processo
        EscribirCentroProceso(lstCampos)

    End Sub

    ''' <summary>
    ''' Método que abre o arquivo que vai ser exportado
    ''' </summary>
    ''' <param name="nome">Nome do arquivo (código do documento)</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [maoliveira] 01/06/2009 Criado
    ''' </history>
    Private Sub AbrirArchivoExportado(nome As String)
        ' Recupera o nome do arquivo
        Dim nomeArquivo As String = nome.PadLeft(8, Constantes.AUTOMATA_ARQUIVO_COMPLENTAR_NUMERO)

        ' Cria o arquivo a ser exportado de acordo com o caminho definido no automata concatenado com o nome "CaminhoSaida" definido no arquivo de configuração
        ArchivoExportado = File.CreateText(Me.RutaDeTrabajo & Prosegur.Genesis.Web.Login.Configuraciones.AppSettings("CaminhoSaida").ToString() & nomeArquivo & ".txt")
    End Sub

    ''' <summary>
    ''' Método que escreve os dados do banco, do cliente e da sucursal do documento no arquivo;
    ''' </summary>
    ''' <param name="lstCampos"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [maoliveira] 01/06/2009 Criado
    ''' </history>
    Private Sub EscribirBancoClienteSucursal(lstCampos As Campos)
        Dim codigoBanco As String = String.Empty
        Dim codigoCliente As String = String.Empty
        Dim descBanco As String = String.Empty
        Dim descCliente As String = String.Empty

        If (lstCampos.Count > 0) Then

            ' Verifica se existem o campo 'Banco'
            Dim objcampoB = From c In lstCampos _
                           Where c.Nombre = Constantes.AUTOMATA_FORMULARIO_CAMPO_BANCO _
                           AndAlso c.Elemento IsNot Nothing


            ' Se Existir
            If objcampoB IsNot Nothing AndAlso objcampoB.Count > 0 Then
                ' Recupera o código do banco
                codigoBanco = Right(objcampoB(0).Elemento.IdPS.PadLeft(5, Constantes.AUTOMATA_ARQUIVO_COMPLENTAR_NUMERO), 5)
                ' Recupera a descrição do banco
                descBanco = Left(objcampoB(0).Elemento.Descripcion, 50)
            End If

            ' Verifica se existem o campo 'ClienteOrigen'
            Dim objcampoCO = From c In lstCampos _
                             Where c.Nombre.Contains(Constantes.AUTOMATA_FORMULARIO_CAMPO_CLIENTEORIGEN) _
                             AndAlso c.Elemento IsNot Nothing

            ' Se existir
            If objcampoCO IsNot Nothing AndAlso objcampoCO.Count > 0 Then
                Dim posSeparador As Int16 = objcampoCO(0).Elemento.IdPS.IndexOf("-")
                ' Se existir separador
                If posSeparador > 0 Then
                    ' Adiciona o código do cliente
                    codigoCliente = Right(Left(objcampoCO(0).Elemento.IdPS, posSeparador).PadLeft(5, Constantes.AUTOMATA_ARQUIVO_COMPLENTAR_NUMERO), 5)
                    ' Adicionar o código da Sucursal
                    codigoCliente &= Constantes.AUTOMATA_ARQUIVO_SEPARADOR & Right(Right(objcampoCO(0).Elemento.IdPS, objcampoCO(0).Elemento.IdPS.Length - (posSeparador + 1)).PadLeft(5, Constantes.AUTOMATA_ARQUIVO_COMPLENTAR_NUMERO), 5)
                Else
                    ' Adiciona o código do cliente
                    codigoCliente = Right(objcampoCO(0).Elemento.IdPS.PadLeft(5, Constantes.AUTOMATA_ARQUIVO_COMPLENTAR_NUMERO), 5)
                End If
                ' Recupera a descrição do cliente
                descCliente = Left(objcampoCO(0).Elemento.Descripcion, 50)
            End If

            ' Escreve no arquivo o código e descrição do cliente e do banco, a palavra chave BACLIVA é usada para identificação posterior
            ArchivoExportado.WriteLine(Constantes.AUTOMATA_ARQUIVO_LINHA_VALOR_INICIAL & _
                                   Constantes.AUTOMATA_ARQUIVO_SEPARADOR & _
                                   Constantes.AUTOMATA_ARQUIVO_CHAVE_BACLIVA & _
                                   Constantes.AUTOMATA_ARQUIVO_SEPARADOR & codigoBanco & _
                                   Constantes.AUTOMATA_ARQUIVO_SEPARADOR & codigoCliente & _
                                   Constantes.AUTOMATA_ARQUIVO_SEPARADOR & descBanco & _
                                   Constantes.AUTOMATA_ARQUIVO_SEPARADOR & descCliente)
        End If
    End Sub

    ''' <summary>
    ''' Método que escreve os dados do banco, do cliente e da sucursal do documento no arquivo, os dados sao provenientes do campo extra bacliva;
    ''' </summary>
    ''' <param name="BaclivaValor">Valor do bacliva proveniente do campo extra bacliva</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [vinicius.gama] 20/01/2010 Criado
    ''' </history>
    Private Sub EscribirBacliva(BaclivaValor As String)
        ArchivoExportado.WriteLine(Constantes.AUTOMATA_ARQUIVO_LINHA_VALOR_INICIAL & _
                                   Constantes.AUTOMATA_ARQUIVO_SEPARADOR & _
                                   Constantes.AUTOMATA_ARQUIVO_CHAVE_BACLIVA & _
                                   Constantes.AUTOMATA_ARQUIVO_SEPARADOR & _
                                   BaclivaValor)
    End Sub

    ''' <summary>
    ''' Método que escreve os dados do centro de processo
    ''' </summary>
    ''' <param name="lstCampos">Lista com os campos do formulário</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [maoliveira] 01/06/2009 Criado
    ''' </history>
    Private Sub EscribirCentroProceso(lstCampos As Campos)

        ' Se exitir campos
        If (lstCampos.Count > 0) Then

            ' Verifica se existem o campo 'CentroProcesoDestino'
            Dim objCampo = From c In lstCampos _
                           Where c.Nombre.Contains(Constantes.AUTOMATA_FORMULARIO_CAMPO_CENTROPROCESODESTINO) _
                           AndAlso c.Elemento IsNot Nothing

            ' Se  não existir
            If (objCampo IsNot Nothing AndAlso objCampo.Count = 0) Then
                ' Verifica se existem o campo 'CentroProcesoOrigen'
                objCampo = From c In lstCampos _
                           Where c.Nombre.Contains(Constantes.AUTOMATA_FORMULARIO_CAMPO_CENTROPROCESOORIGEN) _
                           AndAlso c.Elemento IsNot Nothing
            End If

            ' Se existir
            If objCampo IsNot Nothing AndAlso objCampo.Count > 0 Then

                ' Recupera o código da planta
                Dim IdPsPlanta As String = Left(objCampo(0).Elemento.IdPS.PadLeft(6, Constantes.AUTOMATA_ARQUIVO_SEPARADOR), 3)

                ' Escreve no arquivo, o código e a descrição da planta. A palavra PLANSUC é usada para identificação posterior
                ArchivoExportado.WriteLine(Constantes.AUTOMATA_ARQUIVO_LINHA_VALOR_INICIAL & _
                                   Constantes.AUTOMATA_ARQUIVO_SEPARADOR & _
                                   Constantes.AUTOMATA_ARQUIVO_CHAVE_PLANSUC & _
                                   Constantes.AUTOMATA_ARQUIVO_SEPARADOR & IdPsPlanta & _
                                   Constantes.AUTOMATA_ARQUIVO_SEPARADOR & objCampo(0).Elemento.Planta.Descripcion)

                Dim IdPsTCP As String = Right(objCampo(0).Elemento.IdPS.PadLeft(6, Constantes.AUTOMATA_ARQUIVO_SEPARADOR), 3)

                ' Escreve no arquivo, o código e a descrição do centro de processo. A palavra chave CNTRPRC é usada para identificação posterior
                ArchivoExportado.WriteLine(Constantes.AUTOMATA_ARQUIVO_LINHA_VALOR_INICIAL & _
                                   Constantes.AUTOMATA_ARQUIVO_SEPARADOR & _
                                   Constantes.AUTOMATA_ARQUIVO_CHAVE_CNTRPRC & _
                                   Constantes.AUTOMATA_ARQUIVO_SEPARADOR & IdPsTCP & _
                                   Constantes.AUTOMATA_ARQUIVO_SEPARADOR & ExtraerDescripcion(objCampo(0).Elemento.Descripcion))
            End If

        End If

    End Sub


    Private Function ExtraerIdPS(Campo As String, Optional longfija As Boolean = False, Optional largo As Long = 0)
        Return 0
    End Function

    Private Function ExtraerDescripcion(Campo As String, Optional longfija As Boolean = False, Optional largo As Long = 0)

        Dim pos1b As Integer, pos2b As Integer
        ' Recupera a posição do primeiro espaço
        pos1b = InStr(Campo, Constantes.AUTOMATA_ARQUIVO_SEPARADOR)

        ' Se possui tamanho fixo para recupar 
        If longfija Then
            ' Adiciona a posição do primeiro espaço ao tamanho fixo de caracteres
            pos2b = pos1b + largo + 1
        Else
            ' Recupera a posição do segundo espaço
            pos2b = InStr(pos1b + 1, Campo, Constantes.AUTOMATA_ARQUIVO_SEPARADOR)
        End If

        ' Retorna a string desconsiderando o que tem antes de pos2b
        Return Mid(Campo, pos2b + 1, Len(Campo) - pos2b)

    End Function

    ''' <summary>
    ''' Escreve o número externo e a data de gestão do documento
    ''' </summary>
    ''' <param name="lstCampos">Lista de Campos com o número externo</param>
    ''' <param name="fechaGestion">Data de fechamento da gestão</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [maoliveira] 01/06/2009 Criado
    ''' </history>
    Private Sub EscribirNumExternoFechaGestion(lstCampos As Campos, fechaGestion As DateTime)
        Const PREFIXO_NUM_EXTERNO As String = "E"
        Const SUFIXO_NUM_EXTERNO As String = "00"

        ' Se existe
        If (lstCampos.Count > 0) Then
            ' Recupera o Enumeradores.Automata_Formulario_Campo.NumExterno.ToString na lista de campos
            Dim campos = From c In lstCampos _
                         Where c.Nombre.Contains(Constantes.AUTOMATA_FORMULARIO_CAMPO_NUMEXTERNO)
            ' Se existir
            If (campos IsNot Nothing AndAlso campos.Count > 0) Then

                ' Recupera o valor do Enumeradores.Automata_Formulario_Campo.NumExterno.ToString
                Dim numExterno As String = Left(campos(0).Valor.ToString().PadRight(10, Constantes.AUTOMATA_ARQUIVO_SEPARADOR), 10)
                ' Configura a formatação do "NumExterno
                numExterno = PREFIXO_NUM_EXTERNO & Constantes.AUTOMATA_ARQUIVO_SEPARADOR & numExterno & Constantes.AUTOMATA_ARQUIVO_SEPARADOR & SUFIXO_NUM_EXTERNO
                ' Escreve no arquivo o número externo e a data de gestão, a palavra chave RECIBOV é usada para identificação posterior
                ArchivoExportado.WriteLine(Constantes.AUTOMATA_ARQUIVO_LINHA_VALOR_INICIAL & _
                                   Constantes.AUTOMATA_ARQUIVO_SEPARADOR & _
                                   Constantes.AUTOMATA_ARQUIVO_CHAVE_RECIBOV & _
                                   Constantes.AUTOMATA_ARQUIVO_SEPARADOR & numExterno & _
                                   Constantes.AUTOMATA_ARQUIVO_SEPARADOR & fechaGestion.ToString("dd/MM/yyyy"))
            End If
        End If

    End Sub

    ''' <summary>
    ''' Método que escreve a data de criação do documento
    ''' </summary>
    ''' <param name="fecha">Data de criação do documento</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [maoliveira] 01/06/2009 Criado
    ''' </history>
    Private Sub EscribirFecha(fecha As DateTime)
        ' Escreve a data de criação do documento, a palavra chave "HEADERX" é usada para identificação posterior
        ArchivoExportado.WriteLine(Constantes.AUTOMATA_ARQUIVO_LINHA_VALOR_INICIAL & _
                                   Constantes.AUTOMATA_ARQUIVO_SEPARADOR & _
                                   Constantes.AUTOMATA_ARQUIVO_CHAVE_HEADERX & _
                                   Constantes.AUTOMATA_ARQUIVO_SEPARADOR & fecha.ToString())
    End Sub

    ''' <summary>
    ''' Método que escreve a versão do arquivo do automata
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [maoliveira] 01/06/2009 Criado
    ''' </history>
    Private Sub EscribirFormatoExportacion()

        If (Me.FormatoExportacion = Constantes.AUTOMATA_ARQUIVO_VERSAO_SIGII) Then
            ' Escreve a versão do automata, a palavra chave VERSION é usada para identificação posterior
            ArchivoExportado.WriteLine(Constantes.AUTOMATA_ARQUIVO_LINHA_VALOR_INICIAL & _
                                   Constantes.AUTOMATA_ARQUIVO_SEPARADOR & _
                                   Constantes.AUTOMATA_ARQUIVO_CHAVE_VERSION & _
                                   Constantes.AUTOMATA_ARQUIVO_SEPARADOR & _
                                   Constantes.AUTOMATA_ARQUIVO_VERSAO_SIGII & _
                                   Constantes.AUTOMATA_ARQUIVO_SEPARADOR & _
                                   Constantes.AUTOMATA_ARQUIVO_VERSAO)
        Else
            ' Escreve a versão do automata, a palavra chave VERSION é usada para identificação posterior
            ArchivoExportado.WriteLine(Constantes.AUTOMATA_ARQUIVO_LINHA_VALOR_INICIAL & _
                                   Constantes.AUTOMATA_ARQUIVO_SEPARADOR & _
                                   Constantes.AUTOMATA_ARQUIVO_CHAVE_VERSION & _
                                   Constantes.AUTOMATA_ARQUIVO_SEPARADOR & _
                                   Constantes.AUTOMATA_ARQUIVO_VERSAO_RBO & _
                                   Constantes.AUTOMATA_ARQUIVO_SEPARADOR & _
                                   Constantes.AUTOMATA_ARQUIVO_VERSAO)
        End If

    End Sub

    ''' <summary>
    ''' Método que cria a ata do documento
    ''' </summary>
    ''' <remarks></remarks>
    ''' <param name="objDocumento">Dados do documento</param>
    ''' <history>
    ''' [maoliveira] 02/06/2009 Criado
    ''' </history>
    Private Function CrearActa(ByRef objDocumento As Documento) As Boolean

        Dim cp = New Negocio.CentroProceso
        Dim idCP As Negocio.Campo = objDocumento.Formulario.Campos.FirstOrDefault(Function(f) f.Nombre = Constantes.AUTOMATA_FORMULARIO_CAMPO_CENTROPROCESOORIGEN)
        cp.Id = If(idCP IsNot Nothing, idCP.IdValor, 0)
        cp.Realizar()
        cp.Planta.Realizar()

        ' Variáveis usadas no método
        Dim Doc As New Documento
        Dim mensaje As String = String.Empty
        Dim retorno As Boolean = True

        ' Atribui os valores aos campos do documento
        Doc.EsGrupo = False
        Doc.Agrupado = False
        Doc.Fecha = Util.GetDateTime(cp.Planta.CodDelegacionGenesis)
        Doc.FechaGestion = Util.GetDateTime(cp.Planta.CodDelegacionGenesis)
        Doc.Usuario = Me.Usuario
        'Doc.FechaGestion = Date.Now
        'Doc.Usuario = Me.Usuario
        Doc.Formulario = objDocumento.Formulario
        Doc.Origen.Id = objDocumento.Origen.Id

        ' Cria a ata
        Dim msgRetorno As List(Of String) = Doc.CrearActa()

        ' Atribui ao documento, o código dele
        objDocumento.Id = Doc.Id

        ' Se existir
        If Doc.Bultos.Documento.Id > 0 Then

            ' Atribui ao documento dos pacotes, o código dele
            objDocumento.Bultos.Documento.Id = Doc.Bultos.Documento.Id

        End If

        ' Se existir
        If Doc.Primordial.Id > 0 Then

            ' Atribui ao primordial, o código dele
            objDocumento.Primordial.Id = Doc.Primordial.Id

        End If

        ' Define a mensagem de retorno
        If (objDocumento.Id <> 0) Then
            If EnviaCorreoExitoso Then
                InformeDatos &= vbCrLf & String.Format(Traduzir("msg_documento_criar_ata_sucesso"), objDocumento.Id)
            End If
        Else
            If EnviaCorreoExitoso Then
                For Each msg In msgRetorno
                    InformeDatos &= vbCrLf & msg
                Next
            End If
            retorno = False
        End If

        Return retorno

    End Function

#End Region

End Class