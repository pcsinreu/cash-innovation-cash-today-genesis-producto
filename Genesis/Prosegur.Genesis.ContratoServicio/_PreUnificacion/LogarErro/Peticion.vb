Imports System.Xml.Serialization
Imports System.Xml

Namespace LogarErro

    <Serializable()> _
    <XmlType(Namespace:="urn:LogarErro")> _
    <XmlRoot(Namespace:="urn:LogarErro")> _
    Public Class Peticion

        Public Property DescripcionError As String
        Public Property DescripcionOtro As String
        Public Property FechaError As Date
        Public Property CodigoUsuario As String
        Public Property CodigoDelegacion As String

    End Class

End Namespace
