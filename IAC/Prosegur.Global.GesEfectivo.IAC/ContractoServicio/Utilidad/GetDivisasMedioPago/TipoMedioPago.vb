Namespace Utilidad.GetDivisasMedioPago

    ''' <summary>
    ''' Classe TipoMedioPago
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 02/02/2009 Criado
    ''' </history>
    <Serializable()> _
    Public Class TipoMedioPago

        Private _Codigo As String
        Private _Descripcion As String
        Private _MediosPago As MedioPagoColeccion

        Public Property Codigo() As String
            Get
                Return _Codigo
            End Get
            Set(value As String)
                _Codigo = value
            End Set
        End Property

        Public Property Descripcion() As String
            Get
                Return _Descripcion
            End Get
            Set(value As String)
                _Descripcion = value
            End Set
        End Property

        Public Property MediosPago() As MedioPagoColeccion
            Get
                Return _MediosPago
            End Get
            Set(value As MedioPagoColeccion)
                _MediosPago = value
            End Set
        End Property

    End Class

End Namespace