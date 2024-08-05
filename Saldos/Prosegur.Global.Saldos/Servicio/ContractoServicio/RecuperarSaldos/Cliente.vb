Namespace RecuperarSaldos

    ''' <summary>
    ''' Clase Cliente
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [maoliveira] - 07/07/2011 - Creado
    ''' </history>
    Public Class Cliente

#Region "[VARIAVEIS]"

        Private _Codigo As String
        Private _Descripcion As String
        Private _Canales As New Canales

#End Region

#Region "[PROPRIEDADES]"

        ''' <summary>
        ''' Código del Cliente
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
        ''' Descripción del Cliente
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
        ''' Datos de los Canales
        ''' </summary>
        ''' <value>RecuperarSaldos.Canales</value>
        ''' <returns>RecuperarSaldos.Canales</returns>
        ''' <history>[maoliveira] - 07/07/2011 - Creado</history>
        ''' <remarks></remarks>
        Public Property Canales() As Canales
            Get
                Return _Canales
            End Get
            Set(value As Canales)
                _Canales = value
            End Set
        End Property

#End Region

    End Class

End Namespace