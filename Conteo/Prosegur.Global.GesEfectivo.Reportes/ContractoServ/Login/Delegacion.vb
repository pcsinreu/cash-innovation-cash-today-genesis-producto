Namespace Login

    ''' <summary>
    ''' Delegacion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [gustavo.fraga] 15/03/2011 Criado
    ''' </history>
    <Serializable()> _
    Public Class Delegacion

#Region " Construtor "

        Public Sub New()

            _Plantas = New List(Of Planta)

        End Sub

#End Region

#Region " Variáveis "

        Private _Codigo As String
        Private _Descripcion As String
        Private _Plantas As List(Of Planta)

#End Region

#Region " Propriedades "

        Public Property Codigo() As String
            Get
                Return _Codigo
            End Get
            Set(value As String)
                _Codigo = value
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

        Public Property Plantas() As List(Of Planta)
            Get
                Return _Plantas
            End Get
            Set(value As List(Of Planta))
                _Plantas = value
            End Set
        End Property

#End Region

    End Class

End Namespace
