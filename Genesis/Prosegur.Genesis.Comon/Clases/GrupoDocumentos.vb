Imports System.Collections.ObjectModel

Namespace Clases

    <Serializable()>
    Public NotInheritable Class GrupoDocumentos
        Inherits BindableBase

#Region "Variaveis"

        Private _Identificador As String
        Private _Documentos As ObservableCollection(Of Documento)
        Private _Formulario As Formulario
        Private _CuentaOrigen As Cuenta
        Private _CuentaDestino As Cuenta
        Private _Estado As Enumeradores.EstadoDocumento
        Private _EstadosPosibles As ObservableCollection(Of Enumeradores.EstadoDocumento)
        Private _Historico As ObservableCollection(Of HistoricoMovimientoDocumento)
        Private _FechaHoraCreacion As DateTime
        Private _UsuarioCreacion As String
        Private _FechaHoraModificacion As DateTime
        Private _UsuarioModificacion As String
        Private _CodigoComprobante As String
        Private _GrupoDocumentoPadre As GrupoDocumentos
        Private _GrupoTerminosIAC As GrupoTerminosIAC
        Private _Rowver As Nullable(Of Int64)

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

        Public Property Documentos As ObservableCollection(Of Documento)
            Get
                Return _Documentos
            End Get
            Set(value As ObservableCollection(Of Documento))
                SetProperty(_Documentos, value, "Documentos")
            End Set
        End Property

        Public Property Formulario As Formulario
            Get
                Return _Formulario
            End Get
            Set(value As Formulario)
                SetProperty(_Formulario, value, "Formulario")
            End Set
        End Property

        Public Property CuentaOrigen As Cuenta
            Get
                Return _CuentaOrigen
            End Get
            Set(value As Cuenta)
                SetProperty(_CuentaOrigen, value, "CuentaOrigen")
            End Set
        End Property

        Public Property CuentaDestino As Cuenta
            Get
                Return _CuentaDestino
            End Get
            Set(value As Cuenta)
                SetProperty(_CuentaDestino, value, "CuentaDestino")
            End Set
        End Property

        Public Property Estado As Enumeradores.EstadoDocumento
            Get
                Return _Estado
            End Get
            Set(value As Enumeradores.EstadoDocumento)
                SetProperty(_Estado, value, "Estado")
            End Set
        End Property

        Public Property EstadosPosibles As ObservableCollection(Of Enumeradores.EstadoDocumento)
            Get
                Return _EstadosPosibles
            End Get
            Set(value As ObservableCollection(Of Enumeradores.EstadoDocumento))
                SetProperty(_EstadosPosibles, value, "EstadosPosibles")
            End Set
        End Property

        Public Property Historico As ObservableCollection(Of HistoricoMovimientoDocumento)
            Get
                Return _Historico
            End Get
            Set(value As ObservableCollection(Of HistoricoMovimientoDocumento))
                SetProperty(_Historico, value, "Historico")
            End Set
        End Property

        Public Property FechaHoraCreacion As DateTime
            Get
                Return _FechaHoraCreacion
            End Get
            Set(value As DateTime)
                SetProperty(_FechaHoraCreacion, value, "FechaHoraCreacion")
            End Set
        End Property

        Public Property UsuarioCreacion As String
            Get
                Return _UsuarioCreacion
            End Get
            Set(value As String)
                SetProperty(_UsuarioCreacion, value, "UsuarioCreacion")
            End Set
        End Property

        Public Property FechaHoraModificacion As DateTime
            Get
                Return _FechaHoraModificacion
            End Get
            Set(value As DateTime)
                SetProperty(_FechaHoraModificacion, value, "FechaHoraModificacion")
            End Set
        End Property

        Public Property UsuarioModificacion As String
            Get
                Return _UsuarioModificacion
            End Get
            Set(value As String)
                SetProperty(_UsuarioModificacion, value, "UsuarioModificacion")
            End Set
        End Property

        Public Property CodigoComprobante As String
            Get
                Return _CodigoComprobante
            End Get
            Set(value As String)
                SetProperty(_CodigoComprobante, value, "CodigoComprobante")
            End Set
        End Property

        Public Property GrupoDocumentoPadre As GrupoDocumentos
            Get
                Return _GrupoDocumentoPadre
            End Get
            Set(value As GrupoDocumentos)
                SetProperty(_GrupoDocumentoPadre, value, "GrupoDocumentoPadre")
            End Set
        End Property

        Public Property GrupoTerminosIAC As GrupoTerminosIAC
            Get
                Return _GrupoTerminosIAC
            End Get
            Set(value As GrupoTerminosIAC)
                SetProperty(_GrupoTerminosIAC, value, "GrupoTerminosIAC")
            End Set
        End Property

        Public ReadOnly Property SectorOrigen() As Sector
            Get
                Return If(CuentaOrigen IsNot Nothing, CuentaOrigen.Sector, Nothing)
            End Get
        End Property
        Public ReadOnly Property SectorDestino() As Sector
            Get
                Return If(CuentaDestino IsNot Nothing, CuentaDestino.Sector, Nothing)
            End Get
        End Property

        Public Property Rowver As Nullable(Of Int64)
            Get
                Return _Rowver
            End Get
            Set(value As Nullable(Of Int64))
                SetProperty(_Rowver, value, "Rowver")
            End Set
        End Property

#End Region

    End Class

End Namespace
