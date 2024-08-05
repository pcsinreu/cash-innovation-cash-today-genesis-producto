Namespace Login.ObtenerDelegaciones

    <Serializable()> _
    Public Class Delegacion

#Region "Variáveis"

        Private _codigoDelegacion As String
        Private _nombreDelegacion As String
        Private _oidDelegacion As String
        Private _plantas As PlantaColeccion
#End Region

#Region "Propriedades"

        Public Property CodigoDelegacion() As String
            Get
                Return _codigoDelegacion
            End Get
            Set(value As String)
                _codigoDelegacion = value
            End Set
        End Property

        Public Property NombreDelegacion() As String
            Get
                Return _nombreDelegacion
            End Get
            Set(value As String)
                _nombreDelegacion = value
            End Set
        End Property

        Public Property OidDelegacion() As String
            Get
                Return _oidDelegacion
            End Get
            Set(value As String)
                _oidDelegacion = value
            End Set
        End Property

        Public Property Plantas() As PlantaColeccion
            Get
                Return _plantas
            End Get
            Set(value As PlantaColeccion)
                _plantas = value
            End Set
        End Property

#End Region

    End Class
End Namespace