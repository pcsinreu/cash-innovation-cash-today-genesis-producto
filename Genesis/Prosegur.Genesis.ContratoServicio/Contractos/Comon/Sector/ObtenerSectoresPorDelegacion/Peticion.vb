Imports System.Xml.Serialization
Imports System.Xml

Namespace Contractos.Comon.Sector.ObtenerSectoresPorDelegacion

    <XmlType(Namespace:="urn:ObtenerSectoresPorDelegacion")> _
    <XmlRoot(Namespace:="urn:ObtenerSectoresPorDelegacion")> _
    <Serializable()> _
    Public Class Peticion

#Region "[PROPRIEDADES]"
        Public Property codigoDelegacion As List(Of String)
        Public Property SolamenteTiposSectoresMAE As Boolean
        Public Property SolamentePadres As Boolean
#End Region

    End Class

End Namespace
