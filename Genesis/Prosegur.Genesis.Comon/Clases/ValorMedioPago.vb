Imports System.Collections.ObjectModel

Namespace Clases


    ' ***********************************************************************
    '  Modulo:  ValorMedioPago.vb
    '  Descripción: Clase definición ValorMedioPago
    ' ***********************************************************************

    <Serializable()>
    Public Class ValorMedioPago
        Inherits Valor

#Region "Variaveis"

        Private _Cantidad As Int64
        Private _Terminos As ObservableCollection(Of Termino)
        Private _UnidadMedida As UnidadMedida

#End Region

#Region "Propriedades"

        Public Property Cantidad As Int64
            Get
                Return _Cantidad
            End Get
            Set(value As Int64)
                SetProperty(_Cantidad, value, "Cantidad")
            End Set
        End Property

        Public Property Terminos As ObservableCollection(Of Termino)
            Get
                Return _Terminos
            End Get
            Set(value As ObservableCollection(Of Termino))
                SetProperty(_Terminos, value, "Terminos")
            End Set
        End Property

        Public Property UnidadMedida As UnidadMedida
            Get
                Return _UnidadMedida
            End Get
            Set(value As UnidadMedida)
                SetProperty(_UnidadMedida, value, "UnidadMedida")
            End Set
        End Property

#End Region

    End Class

End Namespace
