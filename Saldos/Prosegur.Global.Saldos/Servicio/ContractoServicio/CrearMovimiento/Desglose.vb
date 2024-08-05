Namespace CrearMovimiento

    ''' <summary>
    ''' Clase Desglose
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [maoliveira] - 28/06/2011 - Creado
    ''' </history>
    <Serializable()> _
    Public Class Desglose

#Region "Variáveis"

        Private _CodigoMoneda As String
        Private _CodigoEspecie As String
        Private _Cantidad As Integer
        Private _Importe As Decimal

#End Region

#Region "Propriedades"


        ''' <summary>
        ''' Código de la Denominación
        ''' </summary>
        ''' <value>String</value>
        ''' <returns>String</returns>
        ''' <history>[maoliveira] - 28/06/2011 - Creado</history>
        ''' <remarks></remarks>
        Public Property CodigoEspecie() As String
            Get
                Return _CodigoEspecie
            End Get
            Set(value As String)
                _CodigoEspecie = value
            End Set
        End Property

        ''' <summary>
        ''' Código ISO de la Divisa
        ''' </summary>
        ''' <value>String</value>
        ''' <returns>String</returns>
        ''' <history>[maoliveira] - 28/06/2011 - Creado</history>
        ''' <remarks></remarks>
        Public Property CodigoMoneda() As String
            Get
                Return _CodigoMoneda
            End Get
            Set(value As String)
                _CodigoMoneda = value
            End Set
        End Property


        ''' <summary>
        ''' Cantidad de Especies
        ''' </summary>
        ''' <value>Integer</value>
        ''' <returns>Integer</returns>
        ''' <history>[maoliveira] - 28/06/2011 - Creado</history>
        ''' <remarks></remarks>
        Public Property Cantidad() As Integer
            Get
                Return _Cantidad
            End Get
            Set(value As Integer)
                _Cantidad = value
            End Set
        End Property

        ''' <summary>
        ''' Valor del Importe
        ''' </summary>
        ''' <value>Decimal</value>
        ''' <returns>Decimal</returns>
        ''' <history>[maoliveira] - 28/06/2011 - Creado</history>
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