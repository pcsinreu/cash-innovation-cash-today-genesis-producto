Namespace IngresoContado

    ''' <summary>
    ''' Classe DeclaradoDet
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 10/09/2009 - Criado
    ''' </history>
    <Serializable()> _
    Public Class DeclaradoDet

#Region "Variáveis"

        Private _Unidades As Integer
        Private _codigoDenominacion As String

#End Region

#Region "Propriedades"

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


        ''' <summary>
        ''' Propriedade Denominacion
        ''' </summary>
        ''' <value>String</value>
        ''' <returns>String</returns>
        ''' <remarks></remarks>
        Public Property CodigoDenominacion() As String
            Get
                Return _codigoDenominacion
            End Get
            Set(value As String)
                _codigoDenominacion = value
            End Set
        End Property


#End Region

    End Class

End Namespace