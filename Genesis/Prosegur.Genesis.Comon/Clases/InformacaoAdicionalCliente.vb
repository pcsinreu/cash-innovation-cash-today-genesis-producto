Namespace Clases

    <Serializable()>
    Public NotInheritable Class InformacaoAdicionalCliente
        Inherits BindableBase

#Region "Variaveis"

        Private _Identificador As String
        Private _Codigo As String
        Private _Descripcion As String
        Private _Observacion As String
        Private _EsActivo As Boolean
        Private _Usuario As String
        Private _FechaHoraActualizacion As DateTime
        Private _BolCopiaDeclarados As Boolean
        Private _BolInvisible As Boolean
        Private _BolEspecificoSaldos As Boolean

#End Region

#Region "Propriedades"

        Public Property Identificador As String
            Get
                Return _Identificador
            End Get
            Set(value As String)
                SetProperty(_Identificador, value, "Identificador")
            End Set
        End Property

        Public Property Codigo As String
            Get
                Return _Codigo
            End Get
            Set(value As String)
                SetProperty(_Codigo, value, "Codigo")
            End Set
        End Property

        Public Property Descripcion As String
            Get
                Return _Descripcion
            End Get
            Set(value As String)
                SetProperty(_Descripcion, value, "Descripcion")
            End Set
        End Property

        Public Property Observacion As String
            Get
                Return _Observacion
            End Get
            Set(value As String)
                SetProperty(_Observacion, value, "Observacion")
            End Set
        End Property

        Public Property EsActivo As Boolean
            Get
                Return _EsActivo
            End Get
            Set(value As Boolean)
                SetProperty(_EsActivo, value, "EsActivo")
            End Set
        End Property

        Public Property Usuario As String
            Get
                Return _Usuario
            End Get
            Set(value As String)
                SetProperty(_Usuario, value, "Usuario")
            End Set
        End Property

        Public Property FechaHoraActualizacion As DateTime
            Get
                Return _FechaHoraActualizacion
            End Get
            Set(value As DateTime)
                SetProperty(_FechaHoraActualizacion, value, "FechaHoraActualizacion")
            End Set
        End Property

        Public Property BolCopiaDeclarados As Boolean
            Get
                Return _BolCopiaDeclarados
            End Get
            Set(value As Boolean)
                SetProperty(_BolCopiaDeclarados, value, "BolCopiaDeclarados")
            End Set
        End Property

        Public Property BolInvisible As Boolean
            Get
                Return _BolInvisible
            End Get
            Set(value As Boolean)
                SetProperty(_BolCopiaDeclarados, value, "BolInvisible")
            End Set
        End Property

        Public Property BolEspecificoSaldos As Boolean
            Get
                Return _BolEspecificoSaldos
            End Get
            Set(value As Boolean)
                SetProperty(_BolEspecificoSaldos, value, "BolEspecificoSaldos")
            End Set
        End Property

#End Region

    End Class

End Namespace
