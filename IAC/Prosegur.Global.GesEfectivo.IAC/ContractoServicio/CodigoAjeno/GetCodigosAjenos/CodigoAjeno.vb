
Namespace CodigoAjeno.GetCodigosAjenos

    ''' <summary>
    ''' Classe CodigoAjeno
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [kasantos] 16/04/2013 Criado
    ''' </history>
    <Serializable()> _
    Public Class CodigoAjeno

#Region "[VARIAVEIS]"

        Private _CodTipoTablaGenesis As String
        Private _OidTablaGenesis As String
        Private _CodIdentificador As String
        Private _BolDefecto As Boolean?
        Private _BolActivo As Boolean?

#End Region

#Region "[PROPRIEDADES]"

        Public Property CodTipoTablaGenesis() As String
            Get
                Return _CodTipoTablaGenesis
            End Get
            Set(value As String)
                _CodTipoTablaGenesis = value
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

        Public Property CodIdentificador() As String
            Get
                Return _CodIdentificador
            End Get
            Set(value As String)
                _CodIdentificador = value
            End Set
        End Property

        Public Property BolDefecto() As Boolean?
            Get
                Return _BolDefecto
            End Get
            Set(value As Boolean?)
                _BolDefecto = value
            End Set
        End Property

        Public Property BolActivo() As Boolean?
            Get
                Return _BolActivo
            End Get
            Set(value As Boolean?)
                _BolActivo = value
            End Set
        End Property

#End Region

    End Class
End Namespace
