Namespace IngresoRemesasNuevo

    ''' <summary>
    ''' Classe RemesaOk
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [vinicius.gama] 27/07/2009 Criado
    ''' </history>
    <Serializable()> _
    Public Class RemesaOk

#Region "[VARIÁVEIS]"

        Private _CodigoDelegacion As String
        Private _CodigoPlanta As String
        Private _IdentificadorRemesaOriginal As String
        Private _IdentificadorDocumentoGenerado As String
        Private _Bultos As New BultosOk
        Private _Observaciones As String

#End Region

#Region "[PROPRIEDADES]"

        Public Property CodigoDelegacion() As String
            Get
                Return _CodigoDelegacion
            End Get
            Set(value As String)
                _CodigoDelegacion = value
            End Set
        End Property

        Public Property CodigoPlanta() As String
            Get
                Return _CodigoPlanta
            End Get
            Set(value As String)
                _CodigoPlanta = value
            End Set
        End Property

        Public Property IdentificadorDocumentoGenerado() As String
            Get
                Return _IdentificadorDocumentoGenerado
            End Get
            Set(value As String)
                _IdentificadorDocumentoGenerado = value
            End Set
        End Property

        Public Property IdentificadorRemesaOriginal() As String
            Get
                Return _IdentificadorRemesaOriginal
            End Get
            Set(value As String)
                _IdentificadorRemesaOriginal = value
            End Set
        End Property

        Public Property Bultos() As BultosOk
            Get
                Return _Bultos
            End Get
            Set(value As BultosOk)
                _Bultos = value
            End Set
        End Property

        Public Property Observaciones() As String
            Get
                Return _Observaciones
            End Get
            Set(value As String)
                _Observaciones = value
            End Set
        End Property

#End Region

    End Class

End Namespace