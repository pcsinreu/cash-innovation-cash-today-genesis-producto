Imports System.Xml.Serialization
Imports System.Runtime.Serialization

Namespace Login.CrearTokenAcceso

    <Serializable()> _
    <XmlType(Namespace:="urn:CrearTokenAcceso")> _
    <XmlRoot(Namespace:="urn:CrearTokenAcceso")> _
    <DataContract()> _
    Public Class Permisos

        Public Property Usuario As EjecutarLogin.Usuario
        Public Property Aplicaciones As EjecutarLogin.AplicacionVersionColeccion

    End Class

End Namespace