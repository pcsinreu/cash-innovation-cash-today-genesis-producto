Imports System.Collections.ObjectModel

Namespace Clases

    ' ***********************************************************************
    '  Modulo:  TipoMercancia.vb
    '  Descripción: Clase definición TipoMercancia
    ' ***********************************************************************
    <Serializable()>
    Public Class TipoMercancia
        Inherits BindableBase

#Region "[VARIAVEIS]"

        Private _Identificador As String
        Private _Codigo As String
        Private _Descripcion As String
        Private _Imagen As Byte()
        Private _Seleccionado As Boolean
        Private _SeleccionarTodos As Boolean

#End Region

#Region "[PROPRIEDADES]"

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
        Public Property Identificador As String
            Get
                Return _Identificador
            End Get
            Set(value As String)
                SetProperty(_Identificador, value, "Identificador")
            End Set
        End Property
        Public Property Imagen As Byte()
            Get
                Return _Imagen
            End Get
            Set(value As Byte())
                SetProperty(_Imagen, value, "Imagen")
            End Set
        End Property

#End Region

    End Class

End Namespace