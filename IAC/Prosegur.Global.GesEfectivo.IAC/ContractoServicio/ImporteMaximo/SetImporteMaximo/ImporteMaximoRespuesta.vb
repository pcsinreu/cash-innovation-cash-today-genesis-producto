
Namespace ImporteMaximo.SetImporteMaximo

    ''' <summary>
    ''' Classe ImporteMaximo
    ''' </summary>
    ''' <remarks></remarks>
    
    <Serializable()> _
    Public Class ImporteMaximoRespuesta

#Region "[VARIAVEIS]"

        Private _CodigoError As String
        Private _MensajeError As String
        Private _OidTablaGenesis As String
        Private _CodTipoTablaGenesis As String
        Private _OidImporteMaximo As String
        Private _CodImporteMaximo As String

#End Region

#Region "[PROPRIEDADES]"

        Public Property CodigoError() As Integer
            Get
                Return _CodigoError
            End Get
            Set(value As Integer)
                _CodigoError = value
            End Set
        End Property

        Public Property MensajeError() As String
            Get
                Return _MensajeError
            End Get
            Set(value As String)
                _MensajeError = value
            End Set
        End Property


        Public Property OidTablaGenesis() As String
            Get
                Return _OidTablaGenesis
            End Get
            Set(value As String)
                _OidTablaGenesis = value
            End Set
        End Property

        Public Property CodTipoTablaGenesis() As String
            Get
                Return _CodTipoTablaGenesis
            End Get
            Set(value As String)
                _CodTipoTablaGenesis = value
            End Set
        End Property

        Public Property OidImporteMaximo() As String
            Get
                Return _OidImporteMaximo
            End Get
            Set(value As String)
                _OidImporteMaximo = value
            End Set
        End Property

        Public Property CodImporteMaximo() As String
            Get
                Return _CodImporteMaximo
            End Get
            Set(value As String)
                _CodImporteMaximo = value
            End Set
        End Property

#End Region

    End Class
End Namespace
