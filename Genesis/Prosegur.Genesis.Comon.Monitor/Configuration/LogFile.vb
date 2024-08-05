Imports System.Configuration
Imports System.Text.RegularExpressions

Namespace Configuration
    Public Class LogFile
        Inherits ConfigurationElement

        Public Sub New()
        End Sub

        Public Sub New(RemoteLog As Boolean, MaxRollFileCount As Integer, DateRollEnforced As Boolean, DatePattern As String, MaxFileSize As String, RollOnStartup As Boolean, ScavengeInterval As Integer)
            Me.RemoteLog = RemoteLog
            Me.MaxRollFileCount = MaxRollFileCount
            Me.DateRollEnforced = DateRollEnforced
            Me.DatePattern = DatePattern
            Me.MaxFileSize = MaxFileSize
            Me.RollOnStartup = RollOnStartup
        End Sub

        ''' <summary>
        ''' Indica se o log será remoto ou local.
        ''' </summary>
        <ConfigurationProperty("RemoteLog")>
        Public Property RemoteLog As Boolean
            Get
                Return Me("RemoteLog")
            End Get
            Set(value As Boolean)
                Me("RemoteLog") = value
            End Set
        End Property

        ''' <summary>
        ''' Quantidade máxima de arquivos. Ao atingir este número de arquivos de logs, o sistema irá excluir o arquivo
        ''' mais velho antes de criar um novo.
        ''' </summary>
        <ConfigurationProperty("MaxRollFileCount")>
        Public Property MaxRollFileCount As Integer
            Get
                Return Me("MaxRollFileCount")
            End Get
            Set(value As Integer)
                Me("MaxRollFileCount") = value
            End Set
        End Property

        ''' <summary>
        ''' Forçar a criação de novos arquivos por data. Todo dia ele criará um novo arquivo.
        ''' </summary>
        <ConfigurationProperty("DateRollEnforced")>
        Public Property DateRollEnforced As Boolean
            Get
                Return Me("DateRollEnforced")
            End Get
            Set(value As Boolean)
                Me("DateRollEnforced") = value
            End Set
        End Property

        ''' <summary>
        ''' Formato de data que será utilizado após o nome completo do arquivo.
        ''' <example>
        '''    Formato: .yyyy-MM-dd-HH
        '''    Data e hora atuais: 20/10/2014 08:50
        '''    Arquivo: MyLog.log
        '''    Arquivo com formato: MyLog.log.2014-10-20-08
        ''' </example>
        ''' </summary>
        <ConfigurationProperty("DatePattern")>
        Public Property DatePattern As String
            Get
                Return Me("DatePattern")
            End Get
            Set(value As String)
                Me("DatePattern") = value
            End Set
        End Property

        ''' <summary>
        ''' Tamanho máximo de cada arquivo em KB ou MB.
        ''' <example>
        '''    512KB
        '''    1MB
        ''' </example>
        ''' </summary>
        <ConfigurationProperty("MaxFileSize")>
        Public Property MaxFileSize As String
            Get
                Return Me("MaxFileSize")
            End Get
            Set(value As String)
                Me("MaxFileSize") = value
            End Set
        End Property

        Private _MaxFileSize As Long
        Public ReadOnly Property GetMaxFileSize As Long
            Get
                If _MaxFileSize = 0 Then
                    _MaxFileSize = CalculateMaxFileSize(MaxFileSize)
                End If
                Return _MaxFileSize
            End Get
        End Property

        ''' <summary>
        ''' Se vai criar um novo arquivo ao reiniciar a aplicação ou se vai continuar a utilizar o último.
        ''' </summary>
        <ConfigurationProperty("RollOnStartup")>
        Public Property RollOnStartup As Boolean
            Get
                Return Me("RollOnStartup")
            End Get
            Set(value As Boolean)
                Me("RollOnStartup") = value
            End Set
        End Property

        ''' <summary>
        ''' Tempo em milissegundos que irá verificar o arquivo de log
        ''' e tomar as ações necessárias para criar um novo arquivo, zipar, etc.
        ''' </summary>
        <ConfigurationProperty("ScavengeInterval")>
        Public Property ScavengeInterval As Integer
            Get
                Return Me("ScavengeInterval")
            End Get
            Set(value As Integer)
                Me("ScavengeInterval") = value
            End Set
        End Property

        Private Function CalculateMaxFileSize(value As String) As Long
            Dim oMatch As Match = Regex.Match(value, "([0-9]+)([A-Z]*)")
            Dim valor As Long = Convert.ToInt64(oMatch.Groups(1).Value)
            Dim unidadeMedida As String = oMatch.Groups(2).Value.ToUpper
            Dim valorUnidade As Long = 1
            Select Case unidadeMedida
                Case "B"
                    valorUnidade = 1
                Case "KB"
                    valorUnidade = 1024
                Case "MB"
                    valorUnidade = 1048576
                Case "GB"
                    valorUnidade = 1073741824
                Case "TB"
                    valorUnidade = 1099511627776
                Case "PB"
                    valorUnidade = 1125899906842624
                Case "EB"
                    valorUnidade = 1152921504606840000
            End Select
            Return valor * valorUnidade
        End Function

    End Class
End Namespace