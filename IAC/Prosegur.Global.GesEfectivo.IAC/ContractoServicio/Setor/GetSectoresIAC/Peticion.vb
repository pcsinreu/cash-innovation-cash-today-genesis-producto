Imports System.Xml.Serialization
Imports System.Xml

Namespace Sector.GetSectoresIAC

    ''' <sumary>
    ''' Classe Peticion
    ''' </sumary>
    ''' <remarks></remarks>
    ''' <history>
    ''' marcel.espiritosanto 28/01/2014 Creado
    ''' </history>
    <XmlType(Namespace:="urn:GetSectoresIAC")> _
    <XmlRoot(Namespace:="urn:GetSectoresIAC")> _
    <Serializable()> _
    Public Class Peticion

#Region "Propriedades"

        Public Property CodigoPlanta As String
        Public Property CodigoFormulario As String
        Public Property CodigoTipoSitio As Genesis.Comon.Enumeradores.TipoSitio
        Public Property CodigoDelegacion As String

#End Region

    End Class

End Namespace

