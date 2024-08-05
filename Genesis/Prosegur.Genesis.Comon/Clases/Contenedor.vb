Imports System.Collections.ObjectModel

Namespace Clases

    ' ***********************************************************************
    '  Modulo:  Contenedor.vb
    '  Descripción: Clase definición Contenedor
    ' ***********************************************************************
    <Serializable()>
    Public Class Contenedor
        Inherits Elemento

#Region "VARIAVEIS"
        Private _Elementos As New ObservableCollection(Of Elemento)
        Private _TipoServicio As TipoServicio
        Private _TipoFormato As TipoFormato
        Private _PrecintoAutomatico As String
#End Region

#Region "PROPRIEDADES"

        Public Property Codigo As String
        Public Property AgrupaContenedor As Boolean
        Public Property TipoContenedor As TipoContenedor

        Public Property Elementos As ObservableCollection(Of Elemento)
            Get
                Return _Elementos
            End Get
            Set(value As ObservableCollection(Of Elemento))
                SetProperty(_Elementos, value, "Elementos")
            End Set
        End Property

        Public Property Estado As Enumeradores.EstadoContenedor
        Public Property TipoServicio As TipoServicio
            Get
                Return _TipoServicio
            End Get
            Set(value As TipoServicio)
                SetProperty(_TipoServicio, value, "TipoServicio")
            End Set
        End Property

        Public Property TipoFormato As TipoFormato
            Get
                Return _TipoFormato
            End Get
            Set(value As TipoFormato)
                SetProperty(_TipoFormato, value, "TipoFormato")
            End Set
        End Property

        Public Property FechaVencimiento As Date
        Public Property Posicion As String

        Public Property PrecintoAutomatico As String
            Get
                Return _PrecintoAutomatico
            End Get
            Set(value As String)
                SetProperty(_PrecintoAutomatico, value, "PrecintoAutomatico")
            End Set
        End Property

#End Region

    End Class

End Namespace
