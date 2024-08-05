Imports System.Xml.Serialization
Imports System.Xml

Namespace SubCliente.SetSubClientes

    <XmlType(Namespace:="urn:SetSubClientes")> _
    <XmlRoot(Namespace:="urn:SetSubClientes")> _
    <Serializable()> _
    Public Class Peticion

        Public Property SubClientes As SubClienteColeccion
        Public Property CodigoUsuario As String
        Public Property BolBaja As Boolean
        Public Property BolEliminaCodigosAjenos As Boolean

    End Class

End Namespace
