Imports System.Xml
Imports System.Xml.Serialization

Namespace GenesisSaldos.Contenedores.ConsultarContenedoresCliente

    ''' <summary>
    ''' Classe Contenedor
    ''' </summary>
    ''' <remarks></remarks>

    <XmlType(Namespace:="urn:ConsultarContenedoresCliente")> _
    <XmlRoot(Namespace:="urn:ConsultarContenedoresCliente")> _
    <Serializable()> _
    Public Class Contenedor

#Region "[PROPRIEDADES]"

        Public Property codTipoContenedor As String
        Public Property codEstadoContenedor As String
        Public Property fechaHoraArmadoDesde As DateTime
        Public Property fechaHoraArmadoHasta As DateTime
        Public Property packModular As String

#End Region

    End Class
End Namespace