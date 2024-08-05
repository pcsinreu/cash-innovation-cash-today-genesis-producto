Namespace Divisa.GetDivisas

    ''' <summary>
    ''' Classe divisa
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 27/01/2009 Criado
    ''' </history>
    <Serializable()> _
    Public Class Divisa

#Region "[Variáveis]"

        Private _CodigoIso As String
        Private _Descripcion As String
        Private _CodigoSimbolo As String
        Private _Vigente As Boolean
        Private _colorDivisa As String
        Private _codigoAccesoDivisa As String

#End Region

#Region "[Propriedades]"

        Public Property CodigoAccesoDivisa() As String
            Get
                Return _codigoAccesoDivisa
            End Get
            Set(value As String)
                _codigoAccesoDivisa = value
            End Set
        End Property

        Public Property CodigoIso() As String
            Get
                Return _CodigoIso
            End Get
            Set(value As String)
                _CodigoIso = value
            End Set
        End Property

        Public Property ColorDivisa() As String
            Get
                Return _colorDivisa
            End Get
            Set(value As String)
                _colorDivisa = value
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

        Public Property CodigoSimbolo() As String
            Get
                Return _CodigoSimbolo
            End Get
            Set(value As String)
                _CodigoSimbolo = value
            End Set
        End Property

        Public Property Vigente() As Boolean
            Get
                Return _Vigente
            End Get
            Set(value As Boolean)
                _Vigente = value
            End Set
        End Property

#End Region

    End Class

End Namespace