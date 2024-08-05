Imports System.Xml.Serialization
Imports System.Xml

Namespace InformeResultadoContaje.ListarResultadoContaje

    ''' <summary>
    ''' Classe Peticion
    ''' </summary>
    ''' <remarks></remarks>
    <XmlType(Namespace:="urn:ListarResultadoContaje")> _
    <XmlRoot(Namespace:="urn:ListarResultadoContaje")> _
    <Serializable()> _
    Public Class Peticion

#Region "[PROPRIEDADES]"

        Public Property OidRemesa As String

#End Region

    End Class

End Namespace