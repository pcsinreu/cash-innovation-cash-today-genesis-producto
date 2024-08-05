Namespace Planta.GetPlantaDetail

    <Serializable()> _
    Public Class Planta

#Region "[VARIVAVEIS]"

        Private _OidPlanta As String
        Private _OidDelegacion As String
        Private _CodDelegacion As String
        Private _DesDelegacion As String
        Private _CodPlanta As String
        Private _DesPlanta As String
        Private _BolActivo As Boolean
        Private _DesUsuarioCreacion As String
        Private _DesUsuarioModificacion As String
        Private _CodigoAjeno As CodigoAjeno.CodigoAjenoColeccionBase
        Private _ImporteMaximo As ImporteMaximo.ImporteMaximoColeccionBase

#End Region

#Region "[PROPRIEDADE]"

        Public Property OidPlanta() As String
            Get
                Return _OidPlanta
            End Get
            Set(value As String)
                _OidPlanta = value
            End Set
        End Property

        Public Property OidDelegacion() As String
            Get
                Return _OidDelegacion
            End Get
            Set(value As String)
                _OidDelegacion = value
            End Set
        End Property

        Public Property CodDelegacion() As String
            Get
                Return _CodDelegacion
            End Get
            Set(value As String)
                _CodDelegacion = value
            End Set
        End Property

        Public Property DesDelegacion() As String
            Get
                Return _DesDelegacion
            End Get
            Set(value As String)
                _DesDelegacion = value
            End Set
        End Property

        Public Property CodPlanta() As String
            Get
                Return _CodPlanta
            End Get
            Set(value As String)
                _CodPlanta = value
            End Set
        End Property

        Public Property DesPlanta() As String
            Get
                Return _DesPlanta
            End Get
            Set(value As String)
                _DesPlanta = value
            End Set
        End Property

        Public Property BolActivo() As String
            Get
                Return _BolActivo
            End Get
            Set(value As String)
                _BolActivo = value
            End Set
        End Property

        Public Property DesUsuarioCreacion() As String
            Get
                Return _DesUsuarioCreacion
            End Get
            Set(value As String)
                _DesUsuarioCreacion = value
            End Set
        End Property

        Public Property DesUsuarioModificacion() As String
            Get
                Return _DesUsuarioModificacion
            End Get
            Set(value As String)
                _DesUsuarioModificacion = value
            End Set
        End Property

        Public Property CodigosAjenos() As CodigoAjeno.CodigoAjenoColeccionBase
            Get
                Return _CodigoAjeno
            End Get
            Set(value As CodigoAjeno.CodigoAjenoColeccionBase)
                _CodigoAjeno = value
            End Set
        End Property

        Public Property ImportesMaximos() As ImporteMaximo.ImporteMaximoColeccionBase
            Get
                Return _ImporteMaximo
            End Get
            Set(value As ImporteMaximo.ImporteMaximoColeccionBase)
                _ImporteMaximo = value
            End Set
        End Property
#End Region

    End Class
End Namespace
