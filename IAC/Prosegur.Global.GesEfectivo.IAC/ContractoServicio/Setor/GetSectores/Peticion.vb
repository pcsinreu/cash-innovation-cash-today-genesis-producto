Imports System.Xml.Serialization
Imports System.Xml

Namespace Setor.GetSectores

    ''' <sumary>
    ''' Classe Peticion
    ''' </sumary>
    ''' <remarks></remarks>
    ''' <history>
    ''' pgoncalves 08/03/2013 Criado
    ''' </history>
    <XmlType(Namespace:="urn:GetSectores")> _
    <XmlRoot(Namespace:="urn:GetSectores")> _
    <Serializable()> _
    Public Class Peticion
        Inherits Paginacion.PeticionPaginacionBase

#Region "[VARIAVEIS]"

        Private _oidPlanta As String
        Private _codSector As String
        Private _desSector As String
        Private _oidSectorPadre As String
        Private _oidTipoSector As String
        Private _bolCentroProceso As Nullable(Of Boolean)
        Private _bolActivo As Nullable(Of Boolean)

#End Region

#Region "[PROPRIEDADES]"

        Public Property oidPlanta() As String
            Get
                Return _oidPlanta
            End Get
            Set(value As String)
                _oidPlanta = value
            End Set
        End Property

        Public Property codSector() As String
            Get
                Return _codSector
            End Get
            Set(value As String)
                _codSector = value
            End Set
        End Property

        Public Property desSector() As String
            Get
                Return _desSector
            End Get
            Set(value As String)
                _desSector = value
            End Set
        End Property

        Public Property oidSectorPadre() As String
            Get
                Return _oidSectorPadre
            End Get
            Set(value As String)
                _oidSectorPadre = value
            End Set
        End Property

        Public Property oidTipoSector() As String
            Get
                Return _oidTipoSector
            End Get
            Set(value As String)
                _oidTipoSector = value
            End Set
        End Property

        Public Property bolCentroProceso() As Nullable(Of Boolean)
            Get
                Return _bolCentroProceso
            End Get
            Set(value As Nullable(Of Boolean))
                _bolCentroProceso = value
            End Set
        End Property

        Public Property bolActivo() As Nullable(Of Boolean)
            Get
                Return _bolActivo
            End Get
            Set(value As Nullable(Of Boolean))
                _bolActivo = value
            End Set
        End Property
#End Region

    End Class

End Namespace

