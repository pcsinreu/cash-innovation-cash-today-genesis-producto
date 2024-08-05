Namespace RecuperarSaldos

    ''' <summary>
    ''' Clase Sector
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [maoliveira] - 07/07/2011 - Creado
    ''' </history>
    Public Class Sector

#Region "[VARIÁVEIS]"

        Private _Codigo As String
        Private _Descripcion As String
        Private _Clientes As New Clientes

#End Region

#Region "[PROPRIEDADES]"

        ''' <summary>
        ''' Código del Sector
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
        ''' Descripción del Sector
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
        ''' Datos de los Clientes
        ''' </summary>
        ''' <value>RecuperarSaldos.Clientes</value>
        ''' <returns>RecuperarSaldos.Clientes</returns>
        ''' <history>[maoliveira] - 07/07/2011 - Creado</history>
        ''' <remarks></remarks>
        Public Property Clientes() As Clientes
            Get
                Return _Clientes
            End Get
            Set(value As Clientes)
                _Clientes = value
            End Set
        End Property

#End Region

    End Class

End Namespace
