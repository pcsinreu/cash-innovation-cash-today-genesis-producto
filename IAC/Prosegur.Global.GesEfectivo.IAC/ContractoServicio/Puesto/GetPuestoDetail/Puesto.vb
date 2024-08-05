Namespace Puesto.GetPuestoDetail
    <Serializable()> _
    Public Class Puesto

        Public Sub New()
            _Aplicacion = New Aplicacion
        End Sub

#Region "Variáveis"
        Private _CodigoDelegacion As String
        Private _DescripcionDelegacion As String
        Private _CodigoPuesto As String
        Private _CodigoHostPuesto As String
        Private _PuestoVigente As Boolean
        Private _Aplicacion As Aplicacion
#End Region

#Region "Propriedades"

        Public Property CodigoDelegacion() As String
            Get
                Return _CodigoDelegacion
            End Get
            Set(value As String)
                _CodigoDelegacion = value
            End Set
        End Property

        Public Property DescripcionDelegacion() As String
            Get
                Return _DescripcionDelegacion
            End Get
            Set(value As String)
                _DescripcionDelegacion = value
            End Set
        End Property

       
  

        Public Property CodigoPuesto() As String
            Get
                Return _CodigoPuesto
            End Get
            Set(value As String)
                _CodigoPuesto = value
            End Set
        End Property

        Public Property CodigoHostPuesto() As String
            Get
                Return _CodigoHostPuesto
            End Get
            Set(value As String)
                _CodigoHostPuesto = value
            End Set
        End Property

        Public Property PuestoVigente() As Boolean
            Get
                Return _PuestoVigente
            End Get
            Set(value As Boolean)
                _PuestoVigente = value
            End Set
        End Property

        Public Property Aplicacion() As Aplicacion
            Get
                Return _Aplicacion
            End Get
            Set(value As Aplicacion)
                _Aplicacion = value
            End Set
        End Property

#End Region

    End Class
End Namespace