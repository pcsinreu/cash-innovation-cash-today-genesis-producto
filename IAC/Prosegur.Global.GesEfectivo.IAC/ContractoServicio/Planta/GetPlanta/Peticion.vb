Imports System.Xml.Serialization
Imports System.Xml

Namespace Planta.GetPlanta
    ''' <summary>
    ''' Classe peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 20/02/2013 Criado
    ''' </history>
    <XmlType(Namespace:="urn:GetPlanta")> _
    <XmlRoot(Namespace:="urn:GetPlanta")> _
    <Serializable()> _
    Public Class Peticion
        Inherits Paginacion.PeticionPaginacionBase

#Region "[VARIAVEIS]"
        Private _OidDelegacion As String
        Private _CodPlanta As String
        Private _DesPlanta As String
        Private _BolActivo As Nullable(Of Boolean)
#End Region

#Region "[PROPRIEDADES]"

        Public Property oidDelegacion() As String
            Get
                Return _OidDelegacion
            End Get
            Set(value As String)
                _OidDelegacion = value
            End Set
        End Property

        Public Property CodPlanta() As String
            Get
                Return _CodPlanta
            End Get
            Set(value As String)
                _CodPlanta = value
            End Set
        End Property

        Public Property DesPlanta() As String
            Get
                Return _DesPlanta
            End Get
            Set(value As String)
                _DesPlanta = value
            End Set
        End Property

        Public Property BolActivo() As Nullable(Of Boolean)
            Get
                Return _BolActivo
            End Get
            Set(value As Nullable(Of Boolean))
                _BolActivo = value
            End Set
        End Property

#End Region

    End Class
End Namespace
