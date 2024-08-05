Namespace RecuperarTransaccionDetallada

    ''' <summary>
    ''' Clase Especie
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [maoliveira] - 12/07/2011 - Creado
    ''' </history>
    Public Class Especie

#Region "[VARIÁVEIS]"

        Private _Codigo As String
        Private _Descripcion As String
        Private _Cantidad As Integer
        Private _Importe As Decimal

#End Region

#Region "[PROPRIEDADES]"

        ''' <summary>
        ''' Código de la Especie
        ''' </summary>
        ''' <value>String</value>
        ''' <returns>String</returns>
        ''' <history>[maoliveira] - 12/07/2011 - Creado</history>
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
        ''' Descripción del Canal
        ''' </summary>
        ''' <value>String</value>
        ''' <returns>String</returns>
        ''' <history>[maoliveira] - 12/07/2011 - Creado</history>
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
        ''' Cantidad de Especies
        ''' </summary>
        ''' <value>String</value>
        ''' <returns>String</returns>
        ''' <history>[maoliveira] - 12/07/2011 - Creado</history>
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
        ''' <value>String</value>
        ''' <returns>String</returns>
        ''' <history>[maoliveira] - 12/07/2011 - Creado</history>
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