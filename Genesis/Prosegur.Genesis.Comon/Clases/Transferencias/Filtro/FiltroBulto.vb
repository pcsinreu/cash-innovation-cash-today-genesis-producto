Imports System.Collections.ObjectModel

Namespace Clases.Transferencias

    ''' <summary>
    ''' Classe que representa o Filtro de Remesas
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()>
    Public Class FiltroBulto
        Inherits BindableBase

#Region "Variaveis"

        Private _Identificador As String
        Private _Precintos As ObservableCollection(Of String)
        Private _TipoFormato As TipoFormato
        Private _TipoServicio As TipoServicio
        Private _Estado As Enumeradores.EstadoBulto
        Private _FechaAltaDesde As Nullable(Of DateTime)
        Private _FechaAltaHasta As Nullable(Of DateTime)

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

        Public Property Precintos As ObservableCollection(Of String)
            Get
                Return _Precintos
            End Get
            Set(value As ObservableCollection(Of String))
                SetProperty(_Precintos, value, "Precintos")
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

        Public Property TipoServicio As TipoServicio
            Get
                Return _TipoServicio
            End Get
            Set(value As TipoServicio)
                SetProperty(_TipoServicio, value, "TipoServicio")
            End Set
        End Property

        Public Property Estado As Enumeradores.EstadoBulto
            Get
                Return _Estado
            End Get
            Set(value As Enumeradores.EstadoBulto)
                SetProperty(_Estado, value, "Estado")
            End Set
        End Property

        Public Property FechaAltaDesde As Nullable(Of DateTime)
            Get
                Return _FechaAltaDesde
            End Get
            Set(value As Nullable(Of DateTime))
                SetProperty(_FechaAltaDesde, value, "FechaAltaDesde")
            End Set
        End Property

        Public Property FechaAltaHasta As Nullable(Of DateTime)
            Get
                Return _FechaAltaHasta
            End Get
            Set(value As Nullable(Of DateTime))
                SetProperty(_FechaAltaHasta, value, "FechaAltaHasta")
            End Set
        End Property

#End Region

    End Class

End Namespace
