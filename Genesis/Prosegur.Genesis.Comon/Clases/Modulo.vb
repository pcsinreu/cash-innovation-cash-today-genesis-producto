Imports System.Collections.ObjectModel

Namespace Clases

    ' ***********************************************************************
    '  Modulo:  Modulo.vb
    '  Descripción: Clase definición Modulo
    ' ***********************************************************************
    <Serializable> _
    Public Class Modulo
        Inherits BindableBase

#Region "[VARIAVEIS]"
        Private _identificador As String
        Private _CodigoTipoModulo As String
        Private _DescripcionTipoModulo As String
        Private _Divisas As ObservableCollection(Of Divisa)
        Private _Cantidad As Integer
        Private _Importe As Decimal
        
#End Region

#Region "[PROPRIEDADES]"
        Public Property Identificador As String
            Get
                Return _identificador
            End Get
            Set(value As String)
                SetProperty(_identificador, value, "identificador")
            End Set
        End Property
        Public Property CodigoTipoModulo As String
            Get
                Return _CodigoTipoModulo
            End Get
            Set(value As String)
                SetProperty(_CodigoTipoModulo, value, "CodigoTipoModulo")
            End Set
        End Property
        Public Property DescripcionTipoModulo As String
            Get
                Return _DescripcionTipoModulo
            End Get
            Set(value As String)
                SetProperty(_DescripcionTipoModulo, value, "DescripcionTipoModulo")
            End Set
        End Property
        Public Property Divisas As ObservableCollection(Of Divisa)
            Get
                Return _Divisas
            End Get
            Set(value As ObservableCollection(Of Divisa))
                SetProperty(_Divisas, value, "Divisas")
            End Set
        End Property

        Public Property Cantidad As Integer
            Get
                Return _Cantidad
            End Get
            Set(value As Integer)
                SetProperty(_Cantidad, value, "Cantidad")
            End Set
        End Property

        Public Property Importe As Integer
            Get
                Return _Importe
            End Get
            Set(value As Integer)
                SetProperty(_Importe, value, "Importe")
            End Set
        End Property
#End Region

    End Class

End Namespace