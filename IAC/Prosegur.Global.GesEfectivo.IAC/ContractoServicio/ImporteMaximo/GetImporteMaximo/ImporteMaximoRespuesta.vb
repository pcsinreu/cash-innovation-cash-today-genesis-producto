
Namespace ImporteMaximo.GetImporteMaximo

    ''' <summary>
    ''' Classe ImporteMaximoRespuesta
    ''' </summary>
    ''' <remarks></remarks>
  
    <Serializable()> _
    Public Class ImporteMaximoRespuesta

#Region "[VARIAVEIS]"

        Private _OidImporteMaximo As String
        Private _CodIdentificador As String
        Private _CodImporteMaximo As String
        Private _DesImporteMaximo As String
        Private _BolDefecto As Boolean
        Private _GmtCreacion As Date
        Private _DesUsuarioCreacion As String
        Private _GmtModificacion As Date
        Private _DesUsuarioModificacion As String

#End Region

#Region "[PROPRIEDADES]"

        Public Property OidImporteMaximo() As String
            Get
                Return _OidImporteMaximo
            End Get
            Set(value As String)
                _OidImporteMaximo = value
            End Set
        End Property

        Public Property CodIdentificador() As String
            Get
                Return _CodIdentificador
            End Get
            Set(value As String)
                _CodIdentificador = value
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

        Public Property DesImporteMaximo() As String
            Get
                Return _DesImporteMaximo
            End Get
            Set(value As String)
                _DesImporteMaximo = value
            End Set
        End Property

        Public Property BolDefecto() As Boolean
            Get
                Return _BolDefecto
            End Get
            Set(value As Boolean)
                _BolDefecto = value
            End Set
        End Property


        Public Property GmtCreacion() As Date
            Get
                Return _GmtCreacion
            End Get
            Set(value As Date)
                _GmtCreacion = value
            End Set
        End Property

        Public Property DesUsuarioCreacion() As String
            Get
                Return _DesUsuarioCreacion
            End Get
            Set(value As String)
                _DesUsuarioCreacion = value
            End Set
        End Property

        Public Property GmtModificacion() As Date
            Get
                Return _GmtModificacion
            End Get
            Set(value As Date)
                _GmtModificacion = value
            End Set
        End Property

        Public Property DesUsuarioModificacion() As String
            Get
                Return _DesUsuarioModificacion
            End Get
            Set(value As String)
                _DesUsuarioModificacion = value
            End Set
        End Property

#End Region

    End Class

End Namespace
