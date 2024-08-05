Namespace Login

    ''' <summary>
    ''' Classe Planta
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()> _
    Public Class Planta

#Region "[VARIAVEIS]"

        Private _Codigo As String
        Private _Descricao As String
        Private _Identificador As String
        Private _TiposSectores As New List(Of TipoSector)
#End Region

#Region "[PROPRIEDADES]"

        Public Property Codigo() As String
            Get
                Return _Codigo
            End Get
            Set(value As String)
                _Codigo = value
            End Set
        End Property

        Public Property Descricao() As String
            Get
                Return _Descricao
            End Get
            Set(value As String)
                _Descricao = value
            End Set
        End Property

        Public Property Identificador() As String
            Get
                Return _Identificador
            End Get
            Set(value As String)
                _Identificador = value
            End Set
        End Property

        Public Property TiposSectores() As List(Of TipoSector)
            Get
                Return _TiposSectores
            End Get
            Set(value As List(Of TipoSector))
                _TiposSectores = value
            End Set
        End Property

#End Region

    End Class

End Namespace