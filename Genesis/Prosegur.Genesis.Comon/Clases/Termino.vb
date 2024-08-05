Imports System.Collections.ObjectModel

Namespace Clases

    ' ***********************************************************************
    '  Modulo:  Termino.vb
    '  Descripción: Definition of the Class Termino
    ' ***********************************************************************
    <Serializable()>
    Public Class Termino
        Inherits BindableBase

#Region "Variaveis"

        Private _Identificador As String
        Private _Codigo As String
        Private _Descripcion As String
        Private _Observacion As String
        Private _ValorInicial As String
        Private _Orden As Integer
        Private _Formato As Formato
        Private _Longitud As Integer
        Private _AlgoritmoValidacion As AlgoritmoValidacion
        Private _Mascara As Mascara
        Private _MostrarDescripcionConCodigo As Boolean
        Private _TieneValoresPosibles As Boolean
        Private _AceptarDigitacion As Boolean
        Private _EstaActivo As Boolean
        Private _EsEspecificoDeSaldos As Boolean
        Private _CodigoUsuario As String
        Private _FechaHoraActualizacion As DateTime
        Private _CodigoMigracion As String
        Private _ValoresPosibles As ObservableCollection(Of TerminoValorPosible)
        Private _Valor As String
        Private _NecIndiceGrupo As Integer

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

        Public Property ValorInicial As String
            Get
                Return _ValorInicial
            End Get
            Set(value As String)
                SetProperty(_ValorInicial, value, "ValorInicial")
            End Set
        End Property

        Public Property Orden As Integer
            Get
                Return _Orden
            End Get
            Set(value As Integer)
                SetProperty(_Orden, value, "Orden")
            End Set
        End Property

        Public Property Formato As Formato
            Get
                Return _Formato
            End Get
            Set(value As Formato)
                SetProperty(_Formato, value, "Formato")
            End Set
        End Property

        Public Property Longitud As Integer
            Get
                Return _Longitud
            End Get
            Set(value As Integer)
                SetProperty(_Longitud, value, "Longitud")
            End Set
        End Property

        Public Property AlgoritmoValidacion As AlgoritmoValidacion
            Get
                Return _AlgoritmoValidacion
            End Get
            Set(value As AlgoritmoValidacion)
                SetProperty(_AlgoritmoValidacion, value, "AlgoritmoValidacion")
            End Set
        End Property

        Public Property Mascara As Mascara
            Get
                Return _Mascara
            End Get
            Set(value As Mascara)
                SetProperty(_Mascara, value, "Mascara")
            End Set
        End Property

        Public Property MostrarDescripcionConCodigo As Boolean
            Get
                Return _MostrarDescripcionConCodigo
            End Get
            Set(value As Boolean)
                SetProperty(_MostrarDescripcionConCodigo, value, "MostrarDescripcionConCodigo")
            End Set
        End Property

        Public Property TieneValoresPosibles As Boolean
            Get
                Return _TieneValoresPosibles
            End Get
            Set(value As Boolean)
                SetProperty(_TieneValoresPosibles, value, "TieneValoresPosibles")
            End Set
        End Property

        Public Property AceptarDigitacion As Boolean
            Get
                Return _AceptarDigitacion
            End Get
            Set(value As Boolean)
                SetProperty(_AceptarDigitacion, value, "AceptarDigitacion")
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

        Public Property EsEspecificoDeSaldos As Boolean
            Get
                Return _EsEspecificoDeSaldos
            End Get
            Set(value As Boolean)
                SetProperty(_EsEspecificoDeSaldos, value, "EsEspecificoDeSaldos")
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

        Public Property CodigoMigracion As String
            Get
                Return _CodigoMigracion
            End Get
            Set(value As String)
                SetProperty(_CodigoMigracion, value, "CodigoMigracion")
            End Set
        End Property

        Public Property ValoresPosibles As ObservableCollection(Of TerminoValorPosible)
            Get
                Return _ValoresPosibles
            End Get
            Set(value As ObservableCollection(Of TerminoValorPosible))
                SetProperty(_ValoresPosibles, value, "ValoresPosibles")
            End Set
        End Property

        Public Property Valor As String
            Get
                Return _Valor
            End Get
            Set(value As String)
                SetProperty(_Valor, value, "Valor")
            End Set
        End Property

        Public Property NecIndiceGrupo As Integer
            Get
                Return _NecIndiceGrupo
            End Get
            Set(value As Integer)
                SetProperty(_NecIndiceGrupo, value, "NecIndiceGrupo")
            End Set
        End Property

#End Region

    End Class

End Namespace


