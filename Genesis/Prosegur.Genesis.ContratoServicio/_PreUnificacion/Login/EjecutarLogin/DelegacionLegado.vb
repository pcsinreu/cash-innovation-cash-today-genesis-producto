Namespace Login.EjecutarLogin

    ''' <summary>
    ''' DelegacionLegado
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  18/01/2011  Criado
    ''' </history>
    <Serializable()> _
    Public Class DelegacionLegado

#Region " Variáveis "

        Private _Aplicacion As String
        Private _Codigo As String

#End Region

#Region "Propriedades"

        Public Property Aplicacion() As String
            Get
                Return _Aplicacion
            End Get
            Set(value As String)
                _Aplicacion = value
            End Set
        End Property

        Public Property Codigo() As String
            Get
                Return _Codigo
            End Get
            Set(value As String)
                _Codigo = value
            End Set
        End Property

#End Region

    End Class

End Namespace