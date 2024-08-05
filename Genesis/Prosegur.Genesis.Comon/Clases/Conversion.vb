Namespace Clases
    ''' <summary>
    ''' Classe de AccionContable.
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()>
    Public Class Conversion
        Inherits BindableBase

#Region "Variaveis"

        Private _identificador As String
        Private _codigo As String
        Private _descripcion As String
        Private _cantidad As Integer
        Private _selecionado As Boolean
        Private _fecha As DateTime

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
                Return _codigo
            End Get
            Set(value As String)
                SetProperty(_codigo, value, "Codigo")
            End Set
        End Property

        Public Property Descripcion As String
            Get
                Return _descripcion
            End Get
            Set(value As String)
                SetProperty(_descripcion, value, "Descripcion")
            End Set
        End Property

        Public Property Cantidad As Integer
            Get
                Return _cantidad
            End Get
            Set(value As Integer)
                SetProperty(_cantidad, value, "Cantidad")
            End Set
        End Property

        Public Property Fecha As DateTime
            Get
                Return _fecha
            End Get
            Set(value As DateTime)
                SetProperty(_fecha, value, "Fecha")
            End Set
        End Property

#End Region

    End Class

End Namespace

