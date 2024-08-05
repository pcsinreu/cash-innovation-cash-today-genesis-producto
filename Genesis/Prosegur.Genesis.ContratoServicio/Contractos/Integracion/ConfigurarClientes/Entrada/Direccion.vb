Imports System.Xml.Serialization
Imports System.ComponentModel

Namespace Contractos.Integracion.ConfigurarClientes.Entrada

    <XmlType(Namespace:="urn:ConfigurarClientes.Entrada")>
    <XmlRoot(Namespace:="urn:ConfigurarClientes.Entrada")>
    <Serializable()>
    Public Class Direccion
        Public Property Pais As String
        Public Property Provincia As String
        Public Property Ciudad As String
        Public Property Email As String
        Public Property Telefono As String
        Public Property CodigoFiscal As String
        Public Property CodigoPostal As String
        Public Property DireccionLinea1 As String
        Public Property DireccionLinea2 As String
        Public Property CampoAdicional1 As String
        Public Property CampoAdicional2 As String
        Public Property CampoAdicional3 As String
        Public Property CategoriaAdicional1 As String
        Public Property CategoriaAdicional2 As String
        Public Property CategoriaAdicional3 As String
    End Class
End Namespace
