Namespace RecuperarSaldos

    ''' <summary>
    ''' Clase Moneda
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [maoliveira] - 07/07/2011 - Creado
    ''' </history>
    Public Class Moneda

#Region "[VARIÁVEIS]"

        Private _Codigo As String
        Private _Descripcion As String
        Private _Importe As Decimal
        Private _Especies As Especies
        Private _Disponible As Boolean

#End Region

#Region "[PROPRIEDADES]"

        ''' <summary>
        ''' Código de la Moneda
        ''' </summary>
        ''' <value>String</value>
        ''' <returns>String</returns>
        ''' <history>[maoliveira] - 07/07/2011 - Creado</history>
        ''' <remarks></remarks>
        Public Property Codigo() As String
            Get
                Return _Codigo
            End Get
            Set(Value As String)
                _Codigo = Value
            End Set
        End Property

        ''' <summary>
        ''' Descripción del Moneda
        ''' </summary>
        ''' <value>String</value>
        ''' <returns>String</returns>
        ''' <history>[maoliveira] - 07/07/2011 - Creado</history>
        ''' <remarks></remarks>
        Public Property Descripcion() As String
            Get
                Return _Descripcion
            End Get
            Set(Value As String)
                _Descripcion = Value
            End Set
        End Property

        ''' <summary>
        ''' Importe total de la Moneda
        ''' </summary>
        ''' <value>Decimal</value>
        ''' <returns>Decimal</returns>
        ''' <history>[maoliveira] - 07/07/2011 - Creado</history>
        ''' <remarks></remarks>
        Public Property Importe() As Decimal
            Get
                Return _Importe
            End Get
            Set(value As Decimal)
                _Importe = value
            End Set
        End Property

        ''' <summary>
        ''' Define si la moneda está o no disponible
        ''' </summary>
        ''' <value>Boolean</value>
        ''' <returns>Boolean</returns>
        ''' <history>[maoliveira] - 06/11/2012 - Creado</history>
        ''' <remarks></remarks>
        Public Property Disponible() As Boolean
            Get
                Return _Disponible
            End Get
            Set(value As Boolean)
                _Disponible = value
            End Set
        End Property

        ''' <summary>
        ''' Datos de las Especies
        ''' </summary>
        ''' <value>RecuperarSaldos.Especies</value>
        ''' <returns>RecuperarSaldos.Especies</returns>
        ''' <history>[maoliveira] - 07/07/2011 - Creado</history>
        ''' <remarks></remarks>
        Public Property Especies() As Especies
            Get
                Return _Especies
            End Get
            Set(value As Especies)
                _Especies = value
            End Set
        End Property

#End Region

    End Class

End Namespace