Namespace Prosegur.Genesis.GenerarReporte.Classes

    Public MustInherit Class Reporte

#Region "Variaveis"

        Private _Parametros As List(Of String)
        Private _ConfigReporte As Comon.Clases.ConfiguracionReporte
        Private _CaminhoArchivos As String
        Private _CaminhoArquivoLog As String
        Private _GenerarLog As Boolean
#End Region

#Region "Propriedades"

        Public Property ConfigReporte As Comon.Clases.ConfiguracionReporte
            Get
                Return _ConfigReporte
            End Get
            Set(value As Comon.Clases.ConfiguracionReporte)
                _ConfigReporte = value
            End Set
        End Property

        Public Property Parametros As List(Of String)
            Get
                Return _Parametros
            End Get
            Set(value As List(Of String))
                _Parametros = value
            End Set
        End Property

        Public Property CaminhoArchivos As String
            Get
                Return _CaminhoArchivos
            End Get
            Set(value As String)
                _CaminhoArchivos = value
            End Set
        End Property

        Public Property CaminhoArquivoLog As String
            Get
                Return _CaminhoArquivoLog
            End Get
            Set(value As String)
                _CaminhoArquivoLog = value
            End Set
        End Property

        Public Property GenerarLog As Boolean
            Get
                Return _GenerarLog
            End Get
            Set(value As Boolean)
                _GenerarLog = value
            End Set
        End Property

#End Region

#Region "Construtor"

        Public Sub New(objParametros As List(Of String), objCaminhoArchivos As String,
                       objCaminhoArchivoLog As String, objGenerarLog As Boolean,
                       objConfigReporte As Comon.Clases.ConfiguracionReporte)

            _Parametros = objParametros
            _CaminhoArchivos = objCaminhoArchivos
            _CaminhoArquivoLog = objCaminhoArchivoLog
            _GenerarLog = objGenerarLog
            _ConfigReporte = objConfigReporte

        End Sub

#End Region

#Region "Métodos"

        Public MustOverride Sub GenerarReporte()

#End Region

    End Class

End Namespace