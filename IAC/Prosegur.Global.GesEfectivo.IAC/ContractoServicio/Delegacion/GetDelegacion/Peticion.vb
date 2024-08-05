Imports System.Xml.Serialization
Imports System.Xml

Namespace Delegacion.GetDelegacion

    ''' <summary>
    ''' Classe peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 06/02/2013 Criado
    ''' </history>
    <XmlType(Namespace:="urn:GetDelegacion")> _
    <XmlRoot(Namespace:="urn:GetDelegacion")> _
    <Serializable()> _
    Public Class Peticion
        Inherits Paginacion.PeticionPaginacionBase

#Region "[VARIAVEIS]"

        Private _CodigoDelegacion As String
        Private _OidPais As String
        Private _Description As String
        Private _Vigente As Nullable(Of Boolean)

#End Region

#Region "[PROPRIEDADE]"

        Public Property CodDelegacion As String
            Get
                Return _CodigoDelegacion
            End Get
            Set(value As String)
                _CodigoDelegacion = value
            End Set
        End Property

        Public Property OidPais() As String
            Get
                Return _OidPais
            End Get
            Set(value As String)
                _OidPais = value
            End Set
        End Property

        Public Property DesDelegacion() As String
            Get
                Return _Description
            End Get
            Set(value As String)
                _Description = value
            End Set
        End Property

        Public Property BolVigente() As Nullable(Of Boolean)
            Get
                Return _Vigente
            End Get
            Set(value As Nullable(Of Boolean))
                _Vigente = value
            End Set
        End Property

#End Region
    End Class
End Namespace
