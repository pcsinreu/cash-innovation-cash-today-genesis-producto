Imports System.Xml.Serialization
Imports System.ComponentModel
Imports System.Runtime.Serialization

Namespace Contractos.Integracion.ConfigurarClientes.Entrada

    <XmlType(Namespace:="urn:ConfigurarClientes.Entrada")>
    <XmlRoot(Namespace:="urn:ConfigurarClientes.Entrada")>
    <Serializable()>
    Public Class Cliente


        '<DefaultValue(Comon.Enumeradores.AccionAB.Alta)>
        <XmlAttribute()>
        <DefaultValue(0)>
        Public Property Accion As Comon.Enumeradores.AccionAB
        Public Property Codigo As String
        Public Property Descripcion As String
        Public Property Tipo As Comon.Enumeradores.TipoCliente
        Public Property CodigoBancario As String
        Public Property BancoCapital As String
        Public Property BancoComision As String
        Public Property PorcComisionCliente As String
        Public Property DatosBancarios As List(Of DatoBancario)
        Public Property CodigosAjenos As List(Of CodigoAjeno)
        Public Property Direccion As Direccion
        Public Property SubClientes As List(Of SubCliente)

    End Class
End Namespace