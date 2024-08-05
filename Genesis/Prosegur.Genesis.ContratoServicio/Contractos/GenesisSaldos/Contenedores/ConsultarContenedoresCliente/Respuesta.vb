Imports System.Xml
Imports System.Xml.Serialization
Imports Prosegur.Genesis.Comon

Namespace GenesisSaldos.Contenedores.ConsultarContenedoresCliente

    ''' <summary>
    ''' Classe Respuesta
    ''' </summary>
    ''' <remarks></remarks>

    <XmlType(Namespace:="urn:ConsultarContenedoresCliente")> _
    <XmlRoot(Namespace:="urn:ConsultarContenedoresCliente")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits BaseRespuesta

#Region "[PROPRIEDADES]"

        Public Property Clientes As List(Of ClienteRespuesta)

#End Region

    End Class
End Namespace