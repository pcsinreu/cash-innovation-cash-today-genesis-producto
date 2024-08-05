Imports System.Xml.Serialization

Namespace Contractos.Integracion.RecuperarClientes
    <XmlType(Namespace:="urn:RecuperarClientes")>
    <XmlRoot(Namespace:="urn:RecuperarClientes")>
    <Serializable()>
    Public Class Peticion

        Public Property Configuracion As Entrada.Configuracion
        Public Property Clientes As List(Of Comon.Entidad)
        Public Property SubClientes As List(Of Comon.Entidad)
        Public Property PuntosServicio As List(Of Comon.Entidad)
        Public Property Nivel As String 'Comon.Enumeradores.TipoNivel
        Public Property RecuperarDatosBancarios As String
        Public Property RecuperarCodigosAjenos As String
        Public Property Paginacion As Comon.Paginacion


    End Class

End Namespace