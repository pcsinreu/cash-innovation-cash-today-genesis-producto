Imports System.ComponentModel
Imports System.Xml.Serialization

Namespace Contractos.Integracion.Comon
    Public Class Paginacion

        <XmlAttribute(DataType:="string", AttributeName:="Indice")>
        <DefaultValue("")>
        Public Property Indice As String

        <XmlAttribute(DataType:="string", AttributeName:="RegistroPorPagina")>
        <DefaultValue("")>
        Public Property RegistroPorPagina As String

    End Class

End Namespace
