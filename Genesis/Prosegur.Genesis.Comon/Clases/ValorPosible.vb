Namespace Clases

    ' ***********************************************************************
    '  Modulo:  ValorPosible.vb
    '  Descripción: Clase definición ValorPosible
    ' ***********************************************************************
    <Serializable()>
    Public Class TerminoValorPosible
        Inherits BindableBase

#Region "Variaveis"

        Private _Identificador As String
        Private _Codigo As String
        Private _Descripcion As String
        Private _ValorDefecto As Boolean
        Private _CodigoUsuario As String
        Private _FechaHoraActualizacion As DateTime
        Private _EstaActivo As Boolean

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

        Public Property ValorDefecto As Boolean
            Get
                Return _ValorDefecto
            End Get
            Set(value As Boolean)
                SetProperty(_ValorDefecto, value, "ValorDefecto")
            End Set
        End Property

        Public Property CodigoUsuario As String
            Get
                Return _CodigoUsuario
            End Get
            Set(value As String)
                SetProperty(_CodigoUsuario, value, "CodigoUsuario")
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

        Public Property EstaActivo As Boolean
            Get
                Return _EstaActivo
            End Get
            Set(value As Boolean)
                SetProperty(_EstaActivo, value, "EstaActivo")
            End Set
        End Property

#End Region

    End Class

End Namespace
