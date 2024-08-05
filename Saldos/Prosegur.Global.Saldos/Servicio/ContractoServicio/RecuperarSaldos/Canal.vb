Namespace RecuperarSaldos

    ''' <summary>
    ''' Clase Canal
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [maoliveira] - 07/07/2011 - Creado
    ''' </history>
    Public Class Canal

#Region "[VARIAVEIS]"

        Private _Codigo As String
        Private _Descripcion As String
        Private _Monedas As New Monedas

#End Region

#Region "[PROPRIEDADES]"

        ''' <summary>
        ''' Código del Canal
        ''' </summary>
        ''' <value>String</value>
        ''' <returns>String</returns>
        ''' <history>[maoliveira] - 07/07/2011 - Creado</history>
        ''' <remarks></remarks>
        Public Property Codigo() As String
            Get
                Return _Codigo
            End Get
            Set(value As String)
                _Codigo = value
            End Set
        End Property

        ''' <summary>
        ''' Descripción del Canal
        ''' </summary>
        ''' <value>String</value>
        ''' <returns>String</returns>
        ''' <history>[maoliveira] - 07/07/2011 - Creado</history>
        ''' <remarks></remarks>
        Public Property Descripcion() As String
            Get
                Return _Descripcion
            End Get
            Set(value As String)
                _Descripcion = value
            End Set
        End Property

        ''' <summary>
        ''' Datos de las Monedas
        ''' </summary>
        ''' <value>RecuperarSaldos.Monedas</value>
        ''' <returns>RecuperarSaldos.Monedas</returns>
        ''' <history>[maoliveira] - 07/07/2011 - Creado</history>
        ''' <remarks></remarks>
        Public Property Monedas() As Monedas
            Get
                Return _Monedas
            End Get
            Set(value As Monedas)
                _Monedas = value
            End Set
        End Property

#End Region

    End Class

End Namespace