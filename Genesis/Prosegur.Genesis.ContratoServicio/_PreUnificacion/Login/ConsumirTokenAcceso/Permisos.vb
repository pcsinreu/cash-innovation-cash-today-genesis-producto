Imports System.Xml.Serialization
Imports System.Runtime.Serialization

Namespace Login.ConsumirTokenAcceso

    <Serializable()> _
    <XmlType(Namespace:="urn:ConsumirTokenAcceso")> _
    <XmlRoot(Namespace:="urn:ConsumirTokenAcceso")> _
    <DataContract()> _
    Public Class Permisos
        Inherits CrearTokenAcceso.Permisos

    End Class

End Namespace