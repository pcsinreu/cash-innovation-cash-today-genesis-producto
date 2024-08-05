Namespace IngresoContado

    ''' <summary>
    ''' Classe Efectivo
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 10/09/2009 - Criado
    ''' </history>
    <Serializable()> _
    Public Class Efectivo

#Region "Variáveis"

        Private _Unidades As Integer
        Private _CodigoISODivisa As String
        Private _CodigoDenominacion As String
        Private _CodigoCalidad As String
        Private _Importe As Decimal

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
        Public Property CodigoCalidad() As String
            Get
                Return _CodigoCalidad
            End Get
            Set(value As String)
                _CodigoCalidad = value
            End Set
        End Property

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property CodigoISODivisa() As String
            Get
                Return _CodigoISODivisa
            End Get
            Set(value As String)
                _CodigoISODivisa = value
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

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property Importe() As Decimal
            Get
                Return _Importe
            End Get
            Set(value As Decimal)
                _Importe = value
            End Set
        End Property

#End Region

    End Class

End Namespace