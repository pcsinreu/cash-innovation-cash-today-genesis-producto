Imports System.Xml.Serialization
Imports System.Xml

Namespace InformeResultadoContaje.BuscarResultadoContaje

    ''' <summary>
    ''' Classe Peticion
    ''' </summary>
    ''' <remarks></remarks>
    <XmlType(Namespace:="urn:BuscarResultadoContaje")> _
    <XmlRoot(Namespace:="urn:BuscarResultadoContaje")> _
    <Serializable()> _
    Public Class Peticion

#Region "[PROPRIEDADES]"

        Public Property CodDelegacion As String
        Public Property CodPrecintoRemesa As String
        Public Property CodPrecintoBulto As String
        Public Property CodTransporte As String
        Public Property CodCliente As String
        Public Property CodSubCliente As String
        Public Property CodPuntoServicio As String
        Public Property FechaInicio As DateTime
        Public Property FechaFin As DateTime
        Public Property EsFechaTransporte As Boolean

#End Region

    End Class

End Namespace