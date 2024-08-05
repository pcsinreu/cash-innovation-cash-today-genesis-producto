Namespace Login.EjecutarLogin

    ''' <summary>
    ''' Pais
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  18/01/2011  Criado
    ''' </history>
    <Serializable()> _
    Public Class Pais

#Region " Variáveis "

        Private _Codigo As String
        Private _Nombre As String
        Private _CodigoISODivisa As String
        Private _Delegaciones As New List(Of Delegacion)

#End Region

#Region "Propriedades"

        Public Property Codigo() As String
            Get
                Return _Codigo
            End Get
            Set(value As String)
                _Codigo = value
            End Set
        End Property

        Public Property Nombre() As String
            Get
                Return _Nombre
            End Get
            Set(value As String)
                _Nombre = value
            End Set
        End Property

        Public Property CodigoISODivisa() As String
            Get
                Return _CodigoISODivisa
            End Get
            Set(value As String)
                _CodigoISODivisa = value
            End Set
        End Property

        Public Property Delegaciones() As List(Of Delegacion)
            Get
                Return _Delegaciones
            End Get
            Set(value As List(Of Delegacion))
                _Delegaciones = value
            End Set
        End Property

#End Region

    End Class

End Namespace