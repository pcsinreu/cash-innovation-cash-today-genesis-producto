Imports System.Xml.Serialization
Imports System.Xml

Namespace Setor.GetSectoresDetail

    ''' <sumary>
    ''' Classe Peticion
    ''' </sumary>
    ''' <remarks></remarks>
    ''' <history>
    ''' pgoncalves 08/03/2013 Criado
    ''' </history>
    <XmlType(Namespace:="urn:GetSectoresDetail")> _
    <XmlRoot(Namespace:="urn:GetSectoresDetail")> _
    <Serializable()> _
    Public Class Peticion
        Inherits Paginacion.PeticionPaginacionBase

#Region "[VARIAVEIS]"

        Private _OidSetor As String

#End Region

#Region "[PROPRIEDADES]"

        Public Property OidSector() As String
            Get
                Return _OidSetor
            End Get
            Set(value As String)
                _OidSetor = value
            End Set
        End Property

#End Region

    End Class

End Namespace

