Namespace Agrupacion.SetAgrupaciones

    ''' <summary>
    ''' Classe Efectivo
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 02/02/2009 Criado
    ''' </history>
    <Serializable()> _
    Public Class Efectivo

        Private _CodigoIsoDivisa As String

        Public Property CodigoIsoDivisa() As String
            Get
                Return _CodigoIsoDivisa
            End Get
            Set(value As String)
                _CodigoIsoDivisa = value
            End Set
        End Property

    End Class

End Namespace