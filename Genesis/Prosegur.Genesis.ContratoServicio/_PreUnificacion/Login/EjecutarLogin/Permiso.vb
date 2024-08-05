Namespace Login.EjecutarLogin

    ''' <summary>
    ''' Permiso
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  18/01/2011  Criado
    ''' </history>
    <Serializable()> _
    Public Class Permiso

#Region " Variáveis "

        Private _Nombre As String
        Private _CodigoAplicacion As String

#End Region

#Region "Propriedades"

        Public Property Nombre() As String
            Get
                Return _Nombre
            End Get
            Set(value As String)
                _Nombre = value
            End Set
        End Property

        Public Property CodigoAplicacion() As String
            Get
                Return _CodigoAplicacion
            End Get
            Set(value As String)
                _CodigoAplicacion = value
            End Set
        End Property


#End Region

    End Class

End Namespace