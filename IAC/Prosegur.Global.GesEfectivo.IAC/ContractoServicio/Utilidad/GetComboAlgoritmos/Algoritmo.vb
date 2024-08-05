Namespace Utilidad.GetComboAlgoritmos

    ''' <summary>
    ''' Classe Algoritmo
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 30/01/2009 Criado
    ''' </history>
    <Serializable()> _
    Public Class Algoritmo

        Private _Codigo As String
        Private _Descripcion As String
        Private _Observaciones As String

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

        Public Property Observaciones() As String
            Get
                Return _Observaciones
            End Get
            Set(value As String)
                _Observaciones = value
            End Set
        End Property

    End Class

End Namespace