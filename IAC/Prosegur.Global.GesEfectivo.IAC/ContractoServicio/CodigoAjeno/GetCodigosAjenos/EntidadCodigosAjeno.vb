
Namespace CodigoAjeno.GetCodigosAjenos

    ''' <summary>
    ''' Classe EntidadCodigosAjeno
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [kasantos] 16/04/2013 Criado
    ''' </history>
    <Serializable()> _
    Public Class EntidadCodigosAjeno

#Region "[VARIAVEIS]"

        Private _OidTablaGenesis As String
        Private _CodTipoTablaGenesis As String
        Private _CodTablaGenesis As String
        Private _DesTablaGenesis As String
        Private _CodigosAjenos As CodigoAjenoRespuestaColeccion

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

        Public Property CodTablaGenesis() As String
            Get
                Return _CodTablaGenesis
            End Get
            Set(value As String)
                _CodTablaGenesis = value
            End Set
        End Property

        Public Property DesTablaGenesis() As String
            Get
                Return _DesTablaGenesis
            End Get
            Set(value As String)
                _DesTablaGenesis = value
            End Set
        End Property

        Public Property CodigosAjenos() As CodigoAjenoRespuestaColeccion
            Get
                Return _CodigosAjenos
            End Get
            Set(value As CodigoAjenoRespuestaColeccion)
                _CodigosAjenos = value
            End Set
        End Property


#End Region

    End Class

End Namespace
