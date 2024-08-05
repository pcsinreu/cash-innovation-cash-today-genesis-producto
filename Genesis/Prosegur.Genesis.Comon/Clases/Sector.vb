Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon.Interfaces.Helper
Imports System.Xml.Serialization

Namespace Clases

    ' ***********************************************************************
    '  Modulo:  Sector.vb
    '  Descripción: Clase definición Sector
    ' ***********************************************************************
    <Serializable()>
    <XmlType(Namespace:="urn:Prosegur.Genesis.Comon.Clases.Sector")> _
    <XmlRoot(Namespace:="urn:Prosegur.Genesis.Comon.Clases.Sector")> _
    Public Class Sector
        Inherits BindableBase
        Implements IEntidadeHelper

        Public Sub New()
            '_TipoSector = New TipoSector
        End Sub
#Region "Variaveis"

        Private _Identificador As String
        Private _SectorPadre As Sector
        Private _TipoSector As TipoSector
        Private _Delegacion As Delegacion
        Private _Planta As Planta
        Private _Codigo As String
        Private _Descripcion As String
        Private _CodigoMigracion As String
        Private _EsCentroProceso As Boolean
        Private _EsPuesto As Boolean
        Private _PermitirDisponerValor As Boolean
        Private _EsTesoro As Boolean
        Private _EsConteo As Boolean
        Private _EsActivo As Boolean
        Private _FechaHoraCreacion As DateTime
        Private _UsuarioCreacion As String
        Private _FechaHoraModificacion As DateTime
        Private _UsuarioModificacion As String
        Private _TiposSector As ObservableCollection(Of TipoSector)
        Private _CodigosAjenos As ObservableCollection(Of CodigoAjeno)

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

        Public Property SectorPadre As Sector
            Get
                Return _SectorPadre
            End Get
            Set(value As Sector)
                SetProperty(_SectorPadre, value, "SectorPadre")
            End Set
        End Property

        Public Property TipoSector As TipoSector
            Get
                Return _TipoSector
            End Get
            Set(value As TipoSector)
                SetProperty(_TipoSector, value, "TipoSector")
            End Set
        End Property

        Public Property Delegacion As Delegacion
            Get
                Return _Delegacion
            End Get
            Set(value As Delegacion)
                SetProperty(_Delegacion, value, "Delegacion")
            End Set
        End Property

        Public Property Planta As Planta
            Get
                Return _Planta
            End Get
            Set(value As Planta)
                SetProperty(_Planta, value, "Planta")
            End Set
        End Property

        Public Property Codigo As String Implements IEntidadeHelper.Codigo
            Get
                Return _Codigo
            End Get
            Set(value As String)
                SetProperty(_Codigo, value, "Codigo")
            End Set
        End Property

        Public Property Descripcion As String Implements IEntidadeHelper.Descripcion
            Get
                Return _Descripcion
            End Get
            Set(value As String)
                SetProperty(_Descripcion, value, "Descripcion")
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

        Public Property EsCentroProceso As Boolean
            Get
                Return _EsCentroProceso
            End Get
            Set(value As Boolean)
                SetProperty(_EsCentroProceso, value, "EsCentroProceso")
            End Set
        End Property

        Public Property PemitirDisponerValor As Boolean
            Get
                Return _PermitirDisponerValor
            End Get
            Set(value As Boolean)
                SetProperty(_PermitirDisponerValor, value, "PemitirDisponerValor")
            End Set
        End Property

        Public Property EsTesoro As Boolean
            Get
                Return _EsTesoro
            End Get
            Set(value As Boolean)
                SetProperty(_EsTesoro, value, "EsTesoro")
            End Set
        End Property

        Public Property EsConteo As Boolean
            Get
                Return _EsTesoro
            End Get
            Set(value As Boolean)
                SetProperty(_EsTesoro, value, "EsTesoro")
            End Set
        End Property

        Public Property EsActivo As Boolean
            Get
                Return _EsActivo
            End Get
            Set(value As Boolean)
                SetProperty(_EsActivo, value, "EsActivo")
            End Set
        End Property

        Public Property EsPuesto As Boolean
            Get
                Return _EsPuesto
            End Get
            Set(value As Boolean)
                SetProperty(_EsPuesto, value, "EsPuesto")
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

        Public Property TiposSector As ObservableCollection(Of TipoSector)
            Get
                Return _TiposSector
            End Get
            Set(value As ObservableCollection(Of TipoSector))
                SetProperty(_TiposSector, value, "TiposSector")
            End Set
        End Property

        Public Property CodigosAjenos As ObservableCollection(Of CodigoAjeno)
            Get
                Return _CodigosAjenos
            End Get
            Set(value As ObservableCollection(Of CodigoAjeno))
                SetProperty(_CodigosAjenos, value, "CodigosAjenos")
            End Set
        End Property

#End Region

    End Class

End Namespace
