Namespace Clases

    ' ***********************************************************************
    '  Modulo:  Calidad.vb
    '  Descripción: Clase definición Calidad
    ' ***********************************************************************

    <Serializable()>
    Public Class Calidad
        Inherits BindableBase

#Region "Variaveis"

        Private _Identificador As String
        Private _Codigo As String
        Private _Descripcion As String
        Private _TipoCalidad As Enumeradores.TipoCalidad

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

        Public Property TipoCalidad As Enumeradores.TipoCalidad
            Get
                Return _TipoCalidad
            End Get
            Set(value As Enumeradores.TipoCalidad)
                SetProperty(_TipoCalidad, value, "TipoCalidad")
            End Set
        End Property

#End Region

    End Class

End Namespace
