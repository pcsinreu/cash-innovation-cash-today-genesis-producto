Namespace Clases

    ' ***********************************************************************
    '  Modulo:  UnidadMedida.vb
    '  Descripción: Clase definición UnidadMedida
    ' ***********************************************************************
    <Serializable()>
    Public Class UnidadMedida
        Inherits BindableBase

#Region "Variaveis"

        Private _Identificador As String
        Private _Codigo As String
        Private _Descripcion As String
        Private _EsPadron As Boolean
        Private _ValorUnidad As Decimal
        Private _TipoUnidadMedida As Enumeradores.TipoUnidadMedida

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

        Public Property EsPadron As Boolean
            Get
                Return _EsPadron
            End Get
            Set(value As Boolean)
                SetProperty(_EsPadron, value, "EsPadron")
            End Set
        End Property

        Public Property ValorUnidad As Decimal
            Get
                Return _ValorUnidad
            End Get
            Set(value As Decimal)
                SetProperty(_ValorUnidad, value, "ValorUnidad")
            End Set
        End Property

        Public Property TipoUnidadMedida As Enumeradores.TipoUnidadMedida
            Get
                Return _TipoUnidadMedida
            End Get
            Set(value As Enumeradores.TipoUnidadMedida)
                SetProperty(_TipoUnidadMedida, value, "TipoUnidadMedida")
            End Set
        End Property

#End Region

    End Class

End Namespace
