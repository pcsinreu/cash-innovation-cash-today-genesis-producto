Imports System.Xml.Serialization
Imports System.Xml

Namespace Sector.GetSectoresTesoro

    ''' <sumary>
    ''' Classe Repuesta
    ''' </sumary>
    ''' <remarks></remarks>
    ''' <history>
    ''' victor.ramos 06/05/2016 - creado
    ''' </history>
    <XmlType(Namespace:="urn:GetSectoresTesoro")> _
    <XmlRoot(Namespace:="urn:GetSectoresTesoro")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "Propriedade"

        Public Property Sectores As List(Of Sector)

#End Region

    End Class
End Namespace


