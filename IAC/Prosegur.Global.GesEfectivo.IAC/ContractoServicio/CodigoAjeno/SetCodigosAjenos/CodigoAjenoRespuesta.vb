
Namespace CodigoAjeno.SetCodigosAjenos

    ''' <summary>
    ''' Classe CodigoAjeno
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [kasantos] 19/04/2013 Criado
    ''' </history>
    <Serializable()> _
    Public Class CodigoAjenoRespuesta

#Region "[VARIAVEIS]"

        Private _CodigoError As String
        Private _MensajeError As String
        Private _OidTablaGenesis As String
        Private _CodTipoTablaGenesis As String
        Private _OidCodigoAjeno As String
        Private _CodAjeno As String

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

        Public Property OidCodigoAjeno() As String
            Get
                Return _OidCodigoAjeno
            End Get
            Set(value As String)
                _OidCodigoAjeno = value
            End Set
        End Property

        Public Property CodAjeno() As String
            Get
                Return _CodAjeno
            End Get
            Set(value As String)
                _CodAjeno = value
            End Set
        End Property

#End Region

    End Class
End Namespace
