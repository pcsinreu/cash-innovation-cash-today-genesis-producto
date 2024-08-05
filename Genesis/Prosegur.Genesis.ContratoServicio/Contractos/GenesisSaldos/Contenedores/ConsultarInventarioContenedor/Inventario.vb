Imports System.Xml
Imports System.Xml.Serialization

Namespace GenesisSaldos.Contenedores.ConsultarInventarioContenedor

    ''' <summary>
    ''' Classe Contenedor
    ''' </summary>
    ''' <remarks></remarks>

    <XmlType(Namespace:="urn:ConsultarInventarioContenedor")> _
    <XmlRoot(Namespace:="urn:ConsultarInventarioContenedor")> _
    <Serializable()> _
    Public Class Inventario

#Region "[PROPRIEDADES]"
        Public codInventario As String
        Public fechaHoraInventarioDesde As DateTime
        Public fechaHoraInventarioHasta As DateTime
        Public Property Sector As Sector
        Public Property Cliente As Cliente
#End Region

    End Class
End Namespace