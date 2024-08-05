Imports System.Collections.ObjectModel

Namespace Clases

    ' ***********************************************************************
    '  Modulo:  MedioPago.vb
    '  Descripción: Clase definición MedioPago
    ' ***********************************************************************
    <Serializable()>
    Public Class MedioPago
        Inherits BindableBase

#Region "Variaveis"

        Private _Identificador As String
        Private _Codigo As String
        Private _Descripcion As String
        Private _Observacion As String
        Private _EstaActivo As Boolean
        Private _CodigoUsuario As String
        Private _FechaHoraActualizacion As DateTime
        Private _Terminos As ObservableCollection(Of Termino)
        Private _Tipo As Enumeradores.TipoMedioPago
        Private _Valores As ObservableCollection(Of ValorMedioPago)
        Private _CodigosAjeno As ObservableCollection(Of Clases.CodigoAjeno)

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

        Public Property Observacion As String
            Get
                Return _Observacion
            End Get
            Set(value As String)
                SetProperty(_Observacion, value, "Observacion")
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

        Public Property Terminos As ObservableCollection(Of Termino)
            Get
                Return _Terminos
            End Get
            Set(value As ObservableCollection(Of Termino))
                SetProperty(_Terminos, value, "Terminos")
            End Set
        End Property

        Public Property Tipo As Enumeradores.TipoMedioPago
            Get
                Return _Tipo
            End Get
            Set(value As Enumeradores.TipoMedioPago)
                SetProperty(_Tipo, value, "Tipo")
            End Set
        End Property

        Public Property Valores As ObservableCollection(Of ValorMedioPago)
            Get
                Return _Valores
            End Get
            Set(value As ObservableCollection(Of ValorMedioPago))
                SetProperty(_Valores, value, "Valores")
            End Set
        End Property

        Public Property CodigosAjeno As ObservableCollection(Of Clases.CodigoAjeno)
            Get
                Return _CodigosAjeno
            End Get
            Set(value As ObservableCollection(Of Clases.CodigoAjeno))
                SetProperty(_CodigosAjeno, value, "CodigosAjeno")
            End Set
        End Property

#End Region

    End Class

End Namespace
