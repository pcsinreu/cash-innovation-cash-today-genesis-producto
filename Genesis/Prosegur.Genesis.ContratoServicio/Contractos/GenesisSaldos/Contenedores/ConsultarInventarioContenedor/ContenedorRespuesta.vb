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
    Public Class ContenedorRespuesta

#Region "[PROPRIEDADES]"

        Public Property codPrecinto As String
        Public Property codInventario As String
        Public Property Cuentas As List(Of Cuenta)
        Public Property clientes As List(Of Cliente)
        Public Property canales As List(Of Canal)

        Public codEstadoContInventario As String

#End Region

    End Class
End Namespace