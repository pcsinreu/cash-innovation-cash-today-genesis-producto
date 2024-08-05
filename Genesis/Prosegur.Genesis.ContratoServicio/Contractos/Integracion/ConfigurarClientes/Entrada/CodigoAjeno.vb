Imports System.Xml.Serialization
Imports System.ComponentModel

Namespace Contractos.Integracion.ConfigurarClientes.Entrada

    <XmlType(Namespace:="urn:ConfigurarClientes.Entrada")>
    <XmlRoot(Namespace:="urn:ConfigurarClientes.Entrada")>
    <Serializable()>
    Public Class CodigoAjeno

        <XmlAttributeAttribute()>
        <DefaultValue(0)>
        Public Property Accion As Comon.Enumeradores.AccionAB
        Public Property CodigoIdentificador As String
        Public Property Codigo As String
        Public Property Descripcion As String
        Public Property Patron As String

    End Class
End Namespace