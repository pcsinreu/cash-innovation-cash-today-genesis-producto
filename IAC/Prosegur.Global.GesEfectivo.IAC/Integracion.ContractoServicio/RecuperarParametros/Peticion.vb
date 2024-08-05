Imports System.Xml.Serialization
Imports System.Xml

Namespace RecuperarParametros

    <XmlType(Namespace:="urn:RecuperarParametros")> _
    <XmlRoot(Namespace:="urn:RecuperarParametros")> _
    <Serializable()> _
    Public Class Peticion

#Region "[PROPRIEDADES]"

        Public Property CodigoPuesto As String
        Public Property CodigoHostPuesto As String
        Public Property CodigoAplicacion As String
        Public Property CodigoDelegacion As String

#End Region

    End Class

End Namespace
