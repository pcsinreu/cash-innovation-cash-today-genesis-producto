Imports System.Xml.Serialization
Imports System.Xml

Namespace Sector.GetSectoresTesoro

    ''' <sumary>
    ''' Classe Peticion
    ''' </sumary>
    ''' <remarks></remarks>
    ''' <history>
    ''' victor.ramos 06/05/2016 Creado
    ''' </history>
    <XmlType(Namespace:="urn:GetSectoresTesoro")> _
    <XmlRoot(Namespace:="urn:GetSectoresTesoro")> _
    <Serializable()> _
    Public Class Peticion

#Region "Propriedades"

        Public Property CodigoDelegacion As String
        Public Property CodigoPlanta As String
        Public Property CodigoTipoSector As String

#End Region

    End Class

End Namespace

