Namespace Login.EjecutarLogin

    ''' <summary>
    ''' Role
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  18/01/2011  Criado
    ''' </history>
    <Serializable()> _
    Public Class Role

#Region " Variáveis "

        Private _Nombre As String
        Private _Timeout As String

#End Region

#Region "Propriedades"

        Public Property Nombre() As String
            Get
                Return _Nombre
            End Get
            Set(value As String)
                _Nombre = value
            End Set
        End Property

        Public Property Timeout() As String
            Get
                Return _Timeout
            End Get
            Set(value As String)
                _Timeout = value
            End Set
        End Property

#End Region

    End Class

End Namespace