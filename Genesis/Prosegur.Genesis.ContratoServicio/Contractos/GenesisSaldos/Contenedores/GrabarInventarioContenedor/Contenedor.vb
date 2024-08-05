Imports System.Xml
Imports System.Xml.Serialization

Namespace GenesisSaldos.Contenedores.GrabarInventarioContenedor

    ''' <summary>
    ''' Classe Contenedor
    ''' </summary>
    ''' <remarks></remarks>

    <XmlType(Namespace:="urn:GrabarInventarioContenedor")> _
    <XmlRoot(Namespace:="urn:GrabarInventarioContenedor")> _
    <Serializable()> _
    Public Class Contenedor

#Region "[PROPRIEDADES]"

        Public codPrecinto As String 'List(Of String)
#End Region

    End Class
End Namespace