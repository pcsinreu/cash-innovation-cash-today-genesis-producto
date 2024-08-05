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
    Public Class Inventario

#Region "[PROPRIEDADES]"
        Public Property codInventario As String
        Public Property regresarDetalles As Boolean
        Public Property Sector As Sector
        Public Property Cliente As Cliente
        Public Property codigosPrecintos As List(Of String)
        Public Property UsuarioCreacion As String

#End Region

    End Class
End Namespace