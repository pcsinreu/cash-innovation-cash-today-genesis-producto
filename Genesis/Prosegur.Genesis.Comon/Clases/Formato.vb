Namespace Clases

    ' ***********************************************************************
    '  Modulo:  Formato.vb
    '  Descripción: Clase definición Formato
    ' ***********************************************************************
    <Serializable()>
    Public Class Formato
        Inherits BindableBase

#Region "Variaveis"

        Private _Identificador As String
        Private _Codigo As String
        Private _Descripcion As String
        Private _CodigoUsuario As String
        Private _FechaHoraActualizacion As DateTime

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

#End Region

    End Class

End Namespace
