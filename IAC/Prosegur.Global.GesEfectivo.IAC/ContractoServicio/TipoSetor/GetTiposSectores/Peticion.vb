Imports System.Xml.Serialization
Imports System.Xml

Namespace TipoSetor.GetTiposSectores

    ''' <summary>
    ''' Classe Peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 01/03/2013 Criado
    ''' </history>
    <XmlType(Namespace:="urn:GetTiposSectores")> _
    <XmlRoot(Namespace:="urn:GetTiposSectores")> _
    <Serializable()> _
    Public Class Peticion
        Inherits Paginacion.PeticionPaginacionBase

#Region "[VARIAVEIS]"

        Private _codTipoSetor As String
        Private _desTipoSetor As String
        Private _BolActivo As Nullable(Of Boolean)
        Private _codCaractTipoSetor As String
        Private _CaractTipoSector As CaracteristicaColeccion

#End Region

#Region "[PROPRIEDADE]"

        Public Property codTipoSector() As String
            Get
                Return _codTipoSetor
            End Get
            Set(value As String)
                _codTipoSetor = value
            End Set
        End Property

        Public Property desTipoSector() As String
            Get
                Return _desTipoSetor
            End Get
            Set(value As String)
                _desTipoSetor = value
            End Set
        End Property

        Public Property bolActivo() As Nullable(Of Boolean)
            Get
                Return _BolActivo
            End Get
            Set(value As Nullable(Of Boolean))
                _BolActivo = value
            End Set
        End Property

        Public Property CaractTipoSector() As CaracteristicaColeccion
            Get
                Return _CaractTipoSector
            End Get
            Set(value As CaracteristicaColeccion)
                _CaractTipoSector = value
            End Set
        End Property

#End Region

    End Class
End Namespace

