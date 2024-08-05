Imports System.Xml.Serialization
Imports System.Xml

Namespace Cliente.SetClientes

    <XmlType(Namespace:="urn:SetClientes")> _
    <XmlRoot(Namespace:="urn:SetClientes")> _
    <Serializable()> _
    Public Class Peticion

        Public Property Clientes As ClienteColeccion
        Public Property CodigoUsuario As String
        Public Property BolBaja As Boolean
        Public Property BolEliminaCodigosAjenos As Boolean = False

        Public Sub New()
            BolEliminaCodigosAjenos = False
        End Sub
    End Class

End Namespace
