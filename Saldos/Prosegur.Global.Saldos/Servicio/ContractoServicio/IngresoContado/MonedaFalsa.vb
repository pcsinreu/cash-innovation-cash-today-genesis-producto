Namespace IngresoContado

    ''' <summary>
    ''' Classe MonedaFalsa
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 10/09/2009 - Criado
    ''' </history>
    <Serializable()> _
    Public Class MonedaFalsa

#Region "Variáveis"

        Private _CodigoDenominacion As String
        Private _Unidades As Integer

#End Region

#Region "Propriedades"

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property CodigoDenominacion() As String
            Get
                Return _CodigoDenominacion
            End Get
            Set(value As String)
                _CodigoDenominacion = value
            End Set
        End Property


        ''' <summary>
        ''' 
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property Unidades() As Integer
            Get
                Return _Unidades
            End Get
            Set(value As Integer)
                _Unidades = value
            End Set
        End Property

#End Region

    End Class

End Namespace
