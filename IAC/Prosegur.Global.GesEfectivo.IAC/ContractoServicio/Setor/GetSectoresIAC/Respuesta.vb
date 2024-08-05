Imports System.Xml.Serialization
Imports System.Xml

Namespace Sector.GetSectoresIAC

    ''' <sumary>
    ''' Classe Repuesta
    ''' </sumary>
    ''' <remarks></remarks>
    ''' <history>
    ''' marcel.espiritosanto 28/01/2014 Creado
    ''' </history>
    <XmlType(Namespace:="urn:GetSectoresIAC")> _
    <XmlRoot(Namespace:="urn:GetSectoresIAC")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits Paginacion.RespuestaPaginacionBase

#Region "Propriedade"

        Public Property Sectores As List(Of Sector)

#End Region

    End Class
End Namespace


